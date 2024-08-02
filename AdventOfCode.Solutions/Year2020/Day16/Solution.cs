namespace AdventOfCode.Solutions.Year2020.Day16;

using System.Text.RegularExpressions;

record Range(int FirstMin, int FirstMax, int SecondMin, int SecondMax)
{
    public bool InRange(int number)
    {
        return (number >= FirstMin && number <= FirstMax)
            || (number >= SecondMin && number <= SecondMax);
    }
}

class Solution : SolutionBase
{
    List<Range> ranges = new List<Range>();
    List<int> values = new List<int>();

    public Solution()
        : base(16, 2020, "")
    {
        string[] paragraphs = Input.SplitByParagraph();

        foreach (Match match in Regex.Matches(paragraphs[0], @"(\d+)-(\d+) or (\d+)-(\d+)"))
        {
            int[] values = match
                .Groups.Cast<Group>()
                .Skip(1)
                .Select(g => int.Parse(g.Value))
                .ToArray();

            ranges.Add(new(values[0], values[1], values[2], values[3]));
        }

        foreach (Match match in Regex.Matches(paragraphs[2], @"\d+"))
        {
            values.Add(int.Parse(match.Value));
        }
    }

    protected override string SolvePartOne()
    {
        int errorRate = 0;

        foreach (int value in values)
        {
            bool valid = false;

            foreach (Range range in ranges)
            {
                if (range.InRange(value))
                {
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                errorRate += value;
            }
        }

        return errorRate.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
