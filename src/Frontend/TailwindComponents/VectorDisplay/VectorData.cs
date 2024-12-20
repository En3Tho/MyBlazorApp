using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TailwindComponents.VectorDisplay;

public static class VectorDataHelpers
{
    public static bool IsSupportedType<T>()
    {
        return
            typeof(T) == typeof(byte)
            || typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(Half)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(float)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(double);
    }
}

public struct VectorElement
{
    public ulong Bits;
    
    public T Read<T>()
        where T : struct
    {
        Debug.Assert(VectorDataHelpers.IsSupportedType<T>());
        
        var span = MemoryMarshal.CreateReadOnlySpan(ref Bits, 1);
        return MemoryMarshal.Cast<ulong, T>(span)[0];
    }

    public void Write<T>(T value)
        where T : struct
    {
        Debug.Assert(VectorDataHelpers.IsSupportedType<T>());
        
        var span = MemoryMarshal.CreateSpan(ref Bits, 1);
        MemoryMarshal.Cast<ulong, T>(span)[0] = value;
    }
}

public record struct UIVectorData(VectorElement Element, Color Color);

public readonly struct UIVector<T>(int bitSize, UIVectorData[] Data)
{
    public readonly UIVectorData[] Data = ValidateOrNormalize(bitSize, Data);
    public readonly int BitSize = bitSize;
    
    private static UIVectorData[] ValidateOrNormalize(int bitSize, UIVectorData[] data)
    {
        static UIVectorData[] ValidateOrNormalizeSize(int maxSize, UIVectorData[] data)
        {
            if (data.Length > maxSize)
                throw new ArgumentException($"Data length is greater than {maxSize}", nameof(data));
            
            if (data.Length == maxSize)
                return data;
            
            var newData = new UIVectorData[maxSize];
            data.CopyTo(newData, 0);
            for (var i = data.Length; i < newData.Length; i++)
            {
                newData[i].Color = data[^1].Color; // fill with last color
            }

            return newData;
        }
        
        if (bitSize is not (128 or 256 or 512))
            throw new ArgumentOutOfRangeException(nameof(bitSize), $"Unsupported vector bit size: {bitSize}");
        
        // Here I want to check if bit size of data.Length * sizeOf(T) * 8 is equal to bitSize
        // if it is lower then we have to extend the data with default values
        // othwerwise throw
        if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
        {
            var maxElements = bitSize / 8;
            return ValidateOrNormalizeSize(maxElements, data);
        }

        if (typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
        {
            var maxElements = bitSize / 16;
            return ValidateOrNormalizeSize(maxElements, data);
        }

        if (typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(float))
        {
            var maxElements = bitSize / 32;
            return ValidateOrNormalizeSize(maxElements, data);
        }

        if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(double))
        {
            var maxElements = bitSize / 64;
            return ValidateOrNormalizeSize(maxElements, data);
        }

        throw new ArgumentException($"Unsupported type: {typeof(T)}");
    }

    public void SetColor(Color color)
    {
        for (var i = 0; i < Data.Length; i++)
        {
            Data[i].Color = color;
        }
    }
    
    public void SetValue(int index, ulong value)
    {
        Data[index].Element.Write(value);
    }
    
    public void SetElement(int index, UIVectorData data)
    {
        Data[index] = data;
    }
    
    // public static UIVector<T> Create<T>(T value)
    // {
    //     
    // }
}