using System.Threading.Channels;

namespace TelemetryProxy;

sealed class ProxyStream : Stream
{
    private readonly Channel<ReadOnlyMemory<byte>> _channel = Channel.CreateBounded<ReadOnlyMemory<byte>>(1);
    private readonly TaskCompletionSource _tcs = new();
    private bool _isDisposed;

    public override void Flush()
    {

    }

    protected override void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _tcs.SetResult();
            _isDisposed = true;
        }
    }

    private ValueTask<int> ReadAsyncCore(ReadOnlyMemory<byte> source, Memory<byte> destination, CancellationToken cancellationToken)
    {
        var length = Math.Min(source.Length, destination.Length);
        source.Span[..length].CopyTo(destination.Span[..length]);
        if (source.Length > length)
        {
            var remaining = source[length..];

            if (!_channel.Writer.TryWrite(remaining))
            {
                // How to amortize this?
                Task.Run(() => _channel.Writer.WriteAsync(remaining, cancellationToken), cancellationToken);
            }
        }

        return ValueTask.FromResult(length);
    }

    private async ValueTask<int> ReadAsyncWithCtsAwait(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var readerTask = _channel.Reader.ReadAsync(cancellationToken).AsTask();
        var completed = await Task.WhenAny(readerTask, _tcs.Task);

        var completion =
            ReferenceEquals(completed, _tcs.Task)
                ? ValueTask.FromResult(0)
                : ReadAsyncCore(readerTask.Result, buffer, cancellationToken);

        return await completion;
    }

    public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = new())
    {
        return _channel.Writer.WriteAsync(buffer, cancellationToken);
    }

    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = new())
    {
        if (_channel.Reader.TryRead(out var source))
        {
            return ReadAsyncCore(source, buffer, cancellationToken);
        }

        return ReadAsyncWithCtsAwait(buffer, cancellationToken);
    }

    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        return ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
    }

    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        return WriteAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
    }

    public override bool CanRead => true;
    public override bool CanWrite => true;

    public override int Read(byte[] buffer, int offset, int count)
    {
        return ReadAsync(buffer, offset, count, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        WriteAsync(buffer, offset, count, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public override bool CanSeek => false;
    public override long Length => throw new NotSupportedException();
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }
}