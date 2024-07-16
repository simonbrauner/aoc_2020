namespace AdventOfCode.Solutions.Year2020.Day09;

class Solution : SolutionBase
{
    const int EarlierNumberCount = 25;
    List<long> numbers = new List<long>();

    public Solution()
        : base(09, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            long number = long.Parse(line);
            numbers.Add(number);
        }
    }

    protected override string SolvePartOne()
    {
        for (int i = EarlierNumberCount; i < numbers.Count(); i++)
        {
            if (!IsSumOfTwoEarlier(i))
            {
                return numbers[i].ToString();
            }
        }
        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    bool IsSumOfTwoEarlier(int i)
    {
        for (int j = i - EarlierNumberCount; j < i; j++)
        {
            for (int k = i - EarlierNumberCount; k < i; k++)
            {
                if (numbers[j] != numbers[k] && numbers[j] + numbers[k] == numbers[i])
                {
                    return true;
                }
            }
        }

        return false;
    }
}
