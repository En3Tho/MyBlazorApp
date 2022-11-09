using System.Diagnostics.CodeAnalysis;

namespace TailwindComponents.CodinGame;

internal record struct Command(int idx1, int idx2, char op)
{
    public static bool TryParse(string value, out Command command)
    {
        var (success, parsedCommand) = value switch
        {
            ['$', var idx1, ' ', ('+' or '-' or '@') and var op, ' ', '$', var idx2] when
                idx1 != idx2
                && char.IsDigit(idx1)
                && char.IsDigit(idx2) => (true, new Command(idx1 - '0', idx2 - '0', op)),
            ['$', var idx1, ' ', ('*' or '/') and var op, ' ', var idx2] when
                idx2 > 0
                && char.IsDigit(idx1)
                && char.IsDigit(idx2) => (true, new (idx1 - '0', idx2 - '0', op)),
            _ => (false, new())
        };

        command = parsedCommand;
        return success;
    }

    public override string ToString()
    {
        var ch = op is '*' or '/' ? "" : "$";
        return $"${idx1} {op} {ch}{idx2}";
    }

    Func<int, int, (int, int)> GetRowProcessor()
    {
        return op switch
        {
            '+' => (a, b) => (a + b, b),
            '-' => (a, b) => (a - b, b),
            '@' => (a, b) => (b, a),
            _ => throw new ArgumentOutOfRangeException(nameof(op))
        };
    }

    Func<int, int, int> GetConstProcessor()
    {
        return op switch
        {
            '*' => (a, b) => a * b,
            '/' => (a, b) => a / b,
            _ => throw new ArgumentOutOfRangeException(nameof(op))
        };
    }

    private bool RunConst(int[,] values, [NotNullWhen(false)] out string? error)
    {
        if ((uint) idx1 >= values.GetLength(0))
        {
            error = "Index out of range";
            return false;
        }

        var constProcessor = GetConstProcessor();
        for (var colIdx = 0; colIdx < values.GetLength(1); colIdx++)
        {
            values[idx1, colIdx] = constProcessor(values[idx1, colIdx], idx2);
        }

        error = null;
        return true;
    }

    private bool RunRow(int[,] values, [NotNullWhen(false)] out string? error)
    {
        if ((uint) idx1 >= values.GetLength(0) || (uint) idx2 >= values.GetLength(0))
        {
            error = "Index out of range";
            return false;
        }

        var rowProcessor = GetRowProcessor();

        for (var colIdx = 0; colIdx < values.GetLength(1); colIdx++)
        {
            var (m1, m2) = rowProcessor(values[idx1, colIdx], values[idx2, colIdx]);
            values[idx1, colIdx] = m1;
            values[idx2, colIdx] = m2;
        }

        error = null;
        return true;
    }


    public bool Run(int[,] values, [NotNullWhen(false)] out string? error)
    {
        if (op is '+' or '-' or '@')
        {
            return RunRow(values, out error);
        }
        return RunConst(values, out error);
    }
}