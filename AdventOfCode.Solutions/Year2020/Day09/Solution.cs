namespace AdventOfCode.Solutions.Year2020.Day09;

class Solution : SolutionBase
{
    const int EarlierNumberCount = 25;
    List<long> numbers = new List<long>();
    long NotSumOfTwoEarlier;

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
            bool isSum = false;

            for (int j = i - EarlierNumberCount; j < i; j++)
            {
                for (int k = j + 1; k < i; k++)
                {
                    if (numbers[j] != numbers[k] && numbers[j] + numbers[k] == numbers[i])
                    {
                        isSum = true;
                    }
                }
            }

            if (!isSum)
            {
                NotSumOfTwoEarlier = numbers[i];
                return numbers[i].ToString();
            }
        }

        return "";
    }

    protected override string SolvePartTwo()
    {
        List<long> cumulativeSums = numbers.CumulativeSums();

        for (int i = 0; i < cumulativeSums.Count(); i++)
        {
            for (int j = i + 3; j < cumulativeSums.Count(); j++)
            {
                if (cumulativeSums[j] - cumulativeSums[i] == NotSumOfTwoEarlier)
                {
                    List<long> range = numbers.GetRange(i + 1, j - i);
                    return (range.Min() + range.Max()).ToString();
                }
            }
        }

        return "";
    }
}
