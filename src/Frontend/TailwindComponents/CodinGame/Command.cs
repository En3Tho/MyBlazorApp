using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TailwindComponents.CodinGame;

internal abstract record Command(int idx1, int idx2, char op)
{
    protected abstract (int, int) PerformOperation(int a, int b);
    protected abstract bool Validate(int[][] matrix, [NotNullWhen(false)] out string? error);

    private abstract record RowOperation(int idx1, int idx2, char op) : Command(idx1, idx2, op)
    {
        protected override bool Validate(int[][] matrix, [NotNullWhen(false)] out string? error)
        {
            if ((uint) idx1 >= matrix.Length || (uint) idx2 >= matrix.Length)
            {
                error = "Index out of range";
                return false;
            }

            error = default;
            return true;
        }

        public sealed override string ToString()
        {
            return $"${idx1} {op} ${idx2}";
        }
    }

    private sealed record AddRows(int idx1, int idx2, char op) : RowOperation(idx1, idx2, op)
    {
        protected override (int, int) PerformOperation(int a, int b)
        {
            return (a + b, b);
        }
    }

    private sealed record SubRows(int idx1, int idx2, char op) : RowOperation(idx1, idx2, op)
    {
        protected override (int, int) PerformOperation(int a, int b)
        {
            return (a - b, b);
        }
    }

    private sealed record SwapRows(int idx1, int idx2, char op) : RowOperation(idx1, idx2, op)
    {
        protected override (int, int) PerformOperation(int a, int b)
        {
            return (b, a);
        }
    }

    private abstract record ConstOperation(int idx1, int idx2, char op) : Command(idx1, idx2, op)
    {
        protected override bool Validate(int[][] matrix, [NotNullWhen(false)] out string? error)
        {
            if ((uint) idx1 >= matrix.Length)
            {
                error = "Index out of range";
                return false;
            }

            error = default;
            return true;
        }

        public sealed override string ToString()
        {
            return $"${idx1} {op} {idx2}";
        }
    }

    private sealed record MulConst(int idx1, int idx2, char op) : ConstOperation(idx1, idx2, op)
    {
        protected override (int, int) PerformOperation(int a, int b)
        {
            return (a * b, b);
        }
    }

    private sealed record DivConst(int idx1, int idx2, char op) : ConstOperation(idx1, idx2, op)
    {
        protected override (int, int) PerformOperation(int a, int b)
        {
            return (a / b, b);
        }
    }

    public bool Run(int[][] matrix, [NotNullWhen(false)] out string? error)
    {
        if (!Validate(matrix, out error))
        {
            return false;
        }

        var row1 = matrix[idx1];
        var row2 = matrix[idx2];
        for (var colIdx = 0; colIdx < row2.Length; colIdx++)
        {
            var (m1, m2) = PerformOperation(row1[colIdx], row2[colIdx]);
            row1[colIdx] = m1;
            row2[colIdx] = m2;
        }

        return true;
    }

    private static Regex TwoVars { get; } = new(@"^\$(\d+)\s*([+-@])\s*\$(\d+)\s*$", RegexOptions.Compiled);
    private static Regex VarConst { get; } = new(@"^\$(\d+)\s*([*\/])\s*(-?\d+)\s*$", RegexOptions.Compiled);

    private static Command? TryTwoVars(string value)
    {
        if (TwoVars.Match(value) is
            { Groups: [_, { ValueSpan: var lvar, }, { ValueSpan: [var op] }, { ValueSpan: var rvar }] })
        {
            if (int.TryParse(lvar, out var idx1)
                && int.TryParse(rvar, out var idx2))
            {
                return op switch
                {
                    '+' => new AddRows(idx1, idx2, op),
                    '-' => new SubRows(idx1, idx2, op),
                    '@' => new SwapRows(idx1, idx2, op),
                    _ => default
                };
            }
        }

        return default;
    }

    private static Command? TryVarConst(string value)
    {
        if (VarConst.Match(value) is { Groups: [_, { ValueSpan: var lvar }, { ValueSpan: [var op] }, { ValueSpan: var rvar }] })
        {
            if (int.TryParse(lvar, out var idx1)
                && int.TryParse(rvar, out var rconst)
                && rconst is not 0)
            {
                return op switch
                {
                    '/' => new DivConst(idx1, rconst, op),
                    '*' => new MulConst(idx1, rconst, op),
                    _ => default
                };
            }
        }

        return default;
    }

    public static Command? TryParse(string value, [NotNullWhen(false)] out string? error)
    {
        var cmd = TryTwoVars(value) ?? TryVarConst(value);
        if (cmd is null)
        {
            error = "Invalid command";
            return default;
        }

        error = null;
        return cmd;
    }
}