namespace AdventOfCode.Solutions.Year2020.Day01;

class Solution : SolutionBase
{
    const int sum = 2020;
    HashSet<int> expenses = new HashSet<int>();

    public Solution()
        : base(01, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            expenses.Add(int.Parse(line));
        }
    }

    protected override string SolvePartOne()
    {
        foreach (int expense in expenses)
        {
            int remainder = sum - expense;

            if (expenses.Contains(remainder))
            {
                return (expense * remainder).ToString();
            }
        }

        return "";
    }

    protected override string SolvePartTwo()
    {
        foreach (int firstExpense in expenses)
        {
            foreach (int secondExpense in expenses)
            {
                int remainder = sum - firstExpense - secondExpense;

                if (expenses.Contains(remainder))
                {
                    return (firstExpense * secondExpense * remainder).ToString();
                }
            }
        }

        return "";
    }
}
