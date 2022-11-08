using System.Diagnostics.CodeAnalysis;

namespace TailwindComponents.CodinGame;

internal record struct Command(int idx1, int idx2, char op)
{
    private static readonly char[] _digits =
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    private static readonly char[] _ops =
    {
        '+', '-', '*', '/'
    };

    public static bool TryParse(string value, out Command command)
    {
        if (value is ['$', var idx1, ' ', var op, ' ', '$', var idx2]
            && idx1 != idx2
            && _digits.Contains(idx1)
            && _digits.Contains(idx2) && _ops.Contains(op))
        {
            command = new(idx1 - '0', idx2 - '0', op);
            return true;
        }

        command = default;
        return false;
    }

    public override string ToString()
    {
        return $"${idx1} {op} ${idx2}";
    }

    Func<int, int, (int, int)> GetProcessor(bool reverse)
    {
        if (reverse)
        {
            return op switch
            {
                '+' => (a, b) => (a - b, b),
                '-' => (a, b) => (a + b, b),
                '*' => (a, b) => (a / b, b),
                '/' => (a, b) => (a * b, b),
                '@' => (a, b) => (a, b),
                _ => throw new ArgumentOutOfRangeException(nameof(op))
            };
        }

        return op switch
        {
            '+' => (a, b) => (a + b, b),
            '-' => (a, b) => (a - b, b),
            '*' => (a, b) => (a * b, b),
            '/' => (a, b) => (a / b, b),
            '@' => (a, b) => (b, a),
            _ => throw new ArgumentOutOfRangeException(nameof(op))
        };
    }

    public bool Run(int[,] values, bool reverse, [NotNullWhen(false)] out string? error)
    {
        if ((uint)idx1 >= values.GetLength(0) || (uint)idx2 >= values.GetLength(0))
        {
            error = "Index out of range";
            return false;
        }

        var processor = GetProcessor(reverse);

        for (var colIdx = 0; colIdx < values.GetLength(1); colIdx++)
        {
            var (m1, m2) = processor(values[idx1, colIdx], values[idx2, colIdx]);
            values[idx1, colIdx] = m1;
            values[idx2, colIdx] = m2;
        }

        error = null;
        return true;
    }
}