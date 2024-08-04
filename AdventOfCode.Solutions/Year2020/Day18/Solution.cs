namespace AdventOfCode.Solutions.Year2020.Day18;

using System.Text.RegularExpressions;

abstract record ExpressionToken()
{
    public static ExpressionToken Create(string stringRepresentation)
    {
        return stringRepresentation[0] switch
        {
            '+' => new Plus(),
            '*' => new Asterisk(),
            '(' => new OpeningParenthesis(),
            ')' => new ClosingParenthesis(),
            _ => new Number(long.Parse(stringRepresentation))
        };
    }
}

record Plus() : ExpressionToken();

record Asterisk() : ExpressionToken();

record OpeningParenthesis() : ExpressionToken();

record ClosingParenthesis() : ExpressionToken();

record Number(long value) : ExpressionToken();

class Solution : SolutionBase
{
    List<Queue<ExpressionToken>> expressions = new List<Queue<ExpressionToken>>();

    public Solution()
        : base(18, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            Queue<ExpressionToken> expression = new Queue<ExpressionToken>();

            foreach (Match match in Regex.Matches(line, @"\+|\*|\(|\)|\d+"))
            {
                expression.Enqueue(ExpressionToken.Create(match.Value));
            }

            expressions.Add(expression);
        }
    }

    protected override string SolvePartOne()
    {
        return expressions.Select(EvaluateExpression).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    long EvaluateExpression(Queue<ExpressionToken> expression)
    {
        long value = NextValue(expression);

        while (expression.Count() > 0)
        {
            ExpressionToken expressionOperator = expression.Dequeue();
            long nextValue = NextValue(expression);

            switch (expressionOperator)
            {
                case Plus:
                    value += nextValue;
                    break;
                case Asterisk:
                    value *= nextValue;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return value;
    }

    long NextValue(Queue<ExpressionToken> expression)
    {
        switch (expression.Dequeue())
        {
            case Number number:
                return number.value;
            case OpeningParenthesis:
                Queue<ExpressionToken> subexpression = new Queue<ExpressionToken>();

                int depth = 1;
                while (true)
                {
                    ExpressionToken current = expression.Dequeue();
                    switch (current)
                    {
                        case OpeningParenthesis:
                            depth++;
                            break;
                        case ClosingParenthesis:
                            depth--;
                            break;
                    }

                    if (depth == 0)
                    {
                        break;
                    }

                    subexpression.Enqueue(current);
                }

                return EvaluateExpression(new Queue<ExpressionToken>(subexpression));
            default:
                throw new InvalidOperationException();
        }
    }
}
