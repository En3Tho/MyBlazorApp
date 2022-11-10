using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TailwindComponents.CodinGame;

internal record struct Command(int idx1, int idx2, char op)
{
    private static Regex TwoVars { get; } = new(@"^\$(\d+)\s*([+-@])\s*\$(\d+)\s*$", RegexOptions.Compiled);
    private static Regex VarConst { get; } = new(@"^\$(\d+)\s*([*\/])\s*(-?\d+)\s*$", RegexOptions.Compiled);

    private static Command? TryTwoVars(string value)
    {
        if (TwoVars.Match(value) is { Groups: [_, { ValueSpan: var lvar, }, { ValueSpan: var op }, { ValueSpan: var rvar }] })
        {
            return new(int.Parse(lvar),
                int.Parse(rvar),
                op[0]);
        }

        return default;
    }

    private static Command? TryVarCon(string value)
    {
        if (VarConst.Match(value) is { Groups: [_, { ValueSpan: var lvar }, { ValueSpan: var op }, { ValueSpan: var rconst }] })
        {
            return new(int.Parse(lvar),
                int.Parse(rconst),
                op[0]);
        }

        return default;
    }

    public static bool TryParse(string value, out Command command, [NotNullWhen(false)] out string? error)
    {
        if (
            (TryTwoVars(value)
            ?? TryVarCon(value)) is {} cmd)
        {
            command = cmd;
            error = null;
            return true;
        }

        error = "Unparsable command";
        command = default;
        return false;
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

    private bool RunConst(int[][] values, [NotNullWhen(false)] out string? error)
    {
        if ((uint) idx1 >= values.Length)
        {
            error = "Index out of range";
            return false;
        }

        if (idx2 == 0)
        {
            error = "Division or multiplication by zero";
            return false;
        }

        var row = values[idx1];
        var constProcessor = GetConstProcessor();
        for (var colIdx = 0; colIdx < row.Length; colIdx++)
        {
            row[colIdx] = constProcessor(row[colIdx], idx2);
        }

        error = null;
        return true;
    }

    private bool RunRow(int[][] values, [NotNullWhen(false)] out string? error)
    {
        if ((uint) idx1 >= values.Length || (uint) idx2 >= values.Length)
        {
            error = "Index out of range";
            return false;
        }

        var rowProcessor = GetRowProcessor();

        var row1 = values[idx1];
        var row2 = values[idx2];
        for (var colIdx = 0; colIdx < row2.Length; colIdx++)
        {
            var (m1, m2) = rowProcessor(row1[colIdx], row2[colIdx]);
            row1[colIdx] = m1;
            row2[colIdx] = m2;
        }

        error = null;
        return true;
    }


    public bool Run(int[][] values, [NotNullWhen(false)] out string? error)
    {
        if (op is '+' or '-' or '@')
        {
            return RunRow(values, out error);
        }

        return RunConst(values, out error);
    }
}