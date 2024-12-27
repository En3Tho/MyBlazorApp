using System.Collections;
using System.Drawing;

namespace TailwindComponents.VectorDisplay;

public abstract class BlockData(Color color)
{
    public Color Color => color; // Maybe a "display descriptor" is better (e.g. color, underlined etc?)
    public abstract BlockData WithColor(Color color);
}

public class BlockData<T>(T value, Color color) : BlockData(color)
    where T : notnull
{
    public T Value => value;
    public override string ToString()
    {
        return Value.ToString()!;
    }

    public override BlockData<T> WithColor(Color color)
    {
        return new(value, color);
    }
}

public readonly struct Block(BlockData[] data)
{
    public BlockData[] Data => data;
    public Block WithColor(Color color)
    {
        return new(data.Select(b => b.WithColor(color)).ToArray());
    }
}

static class Matrix4x4BlockData
{
    public static readonly BlockData X0 = new BlockData<int>(0, Color.Red);
    public static readonly BlockData X1 = new BlockData<int>(1, Color.Red);
    public static readonly BlockData X2 = new BlockData<int>(2, Color.Red);
    public static readonly BlockData X3 = new BlockData<int>(3, Color.Red);
    
    public static readonly BlockData Y0 = new BlockData<int>(0, Color.Blue);
    public static readonly BlockData Y1 = new BlockData<int>(1, Color.Blue);
    public static readonly BlockData Y2 = new BlockData<int>(2, Color.Blue);
    public static readonly BlockData Y3 = new BlockData<int>(3, Color.Blue);
     
    public static readonly BlockData Z0 = new BlockData<int>(0, Color.Green);
    public static readonly BlockData Z1 = new BlockData<int>(1, Color.Green);
    public static readonly BlockData Z2 = new BlockData<int>(2, Color.Green);
    public static readonly BlockData Z3 = new BlockData<int>(3, Color.Green);
     
    public static readonly BlockData V0 = new BlockData<int>(0, Color.Orange);
    public static readonly BlockData V1 = new BlockData<int>(1, Color.Orange);
    public static readonly BlockData V2 = new BlockData<int>(2, Color.Orange);
    public static readonly BlockData V3 = new BlockData<int>(3, Color.Orange);
}

public readonly struct BlockBuilder() : IEnumerable<BlockData>
{
    public List<BlockData> Data { get; } = [ ];
    public void Add<T>((T value, Color color) value) where T : notnull
    {
        Data.Add(new BlockData<T>(value.value, value.color));
    }

    public void Add(BlockData data)
    {
        Data.Add(data);
    }

    public Block Build()
    {
        return new(Data.ToArray());
    }

    public IEnumerator<BlockData> GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class BlockExtensions
{
    public static Block ToBitsBlock(this int value, Color color)
    {
        var bits = new BlockData[32];
        for (var i = 0; i < 32; i++)
        {
            bits[i] = new BlockData<int>(value & (1 << i), color);
        }
        return new(bits);
    }

    public static Block ToBytesBlock(this int value, Color color)
    {
        var bytes = new BlockData[4];
        for (var i = 0; i < 4; i++)
        {
            bytes[i] = new BlockData<byte>((byte)(value >> (i * 8)), color);
        }
        return new(bytes);
    }
}