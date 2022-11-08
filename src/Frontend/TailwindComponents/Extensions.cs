using System.Collections;

namespace TailwindComponents;

public static class IDictionaryExtensions
{
    public static string? TryGetValueAsString(this IDictionary<string, object> dictionary, string key)
    {
        dictionary.TryGetValue(key, out var value);
        return value as string;
    }
}

public static class ArrayExtensions
{
    public static IndexedArray<T> ToIndexable<T>(this T[] array) => new(array);

    public static ArrayIndexedEnumerator<T> GetEnumerator<T>(this T[] array) => new(array);

    public record struct ArrayIndexedEnumerator<T>(T[] array) : IEnumerator<(int, T)>
    {
        private int _index = -1;
        public bool MoveNext()
        {
            _index++;
            return _index < array.Length;
        }

        public void Reset()
        {
            _index = -1;
        }

        public (int, T) Current => (_index, array[_index]);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public readonly record struct IndexedArray<T>(T[] array) : IEnumerable<(int, T)>
    {
        public IEnumerator<(int, T)> GetEnumerator()
        {
            return new ArrayIndexedEnumerator<T>(array);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

public static class RangeExtensions
{
    public static RangeEnumerator GetEnumerator(this Range range) => new(range);

    public ref struct RangeEnumerator
    {
        private readonly int _start;
        private readonly int _length;
        private int _count;
        public RangeEnumerator(Range range)
        {
            var (offset, length) = range.GetOffsetAndLength(int.MaxValue);
            (Current, _start, _length, _count) = (offset - 1, offset - 1, length, 0);
        }

        public bool MoveNext()
        {
            (var result, _count, Current) = (_count < _length, _count + 1, Current + 1);
            return result;
        }

        public void Reset() => (Current, _count) = (_start, 0);

        public int Current { get; private set; }

        public void Dispose() { }
    }
}