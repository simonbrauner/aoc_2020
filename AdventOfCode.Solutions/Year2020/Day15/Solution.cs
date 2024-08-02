namespace AdventOfCode.Solutions.Year2020.Day15;

class Solution : SolutionBase
{
    List<int> startingNumbers;

    public Solution()
        : base(15, 2020, "")
    {
        startingNumbers = new List<int>(Input.Split(',').Select(s => int.Parse(s)));
    }

    protected override string SolvePartOne()
    {
        return NthNumberSpoken(2020).ToString();
    }

    protected override string SolvePartTwo()
    {
        return NthNumberSpoken(30000000).ToString();
    }

    int NthNumberSpoken(int n)
    {
        int turn = 0;
        Dictionary<int, int> mostRecentlySpoken = new Dictionary<int, int>();

        foreach (int startingNumber in startingNumbers)
        {
            turn++;
            mostRecentlySpoken.Add(startingNumber, turn);
        }

        int current = startingNumbers.Last();
        while (turn < n)
        {
            int mostRecentlySpokenCurrent = mostRecentlySpoken.GetValueOrDefault(current);
            mostRecentlySpoken[current] = turn;

            if (mostRecentlySpokenCurrent == 0)
            {
                current = 0;
            }
            else
            {
                current = turn - mostRecentlySpokenCurrent;
            }

            turn++;
        }

        return current;
    }
}
