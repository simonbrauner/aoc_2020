namespace AdventOfCode.Solutions.Year2020.Day10;

class Solution : SolutionBase
{
    List<int> joltages = new List<int>();

    public Solution()
        : base(10, 2020, "")
    {
        joltages.Add(0);

        foreach (string line in Input.SplitByNewline())
        {
            joltages.Add(int.Parse(line));
        }

        joltages.Sort();
        joltages.Add(joltages.Last() + 3);
    }

    protected override string SolvePartOne()
    {
        Dictionary<int, int> differences = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 }
        };

        for (int i = 1; i < joltages.Count(); i++)
        {
            differences[joltages[i] - joltages[i - 1]]++;
        }

        return (differences[1] * differences[3]).ToString();
    }

    protected override string SolvePartTwo()
    {
        long[] ways = new long[joltages.Last() + 1];

        foreach (int joltage in joltages)
        {
            ways[joltage]++;
        }

        for (int i = ways.Count() - 1 - 3; i >= 0; i--)
        {
            if (ways[i] != 0)
            {
                ways[i] = ways[i + 1] + ways[i + 2] + ways[i + 3];
            }
        }

        return ways[0].ToString();
    }
}
