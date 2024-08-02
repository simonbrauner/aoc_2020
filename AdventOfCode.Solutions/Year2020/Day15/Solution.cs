namespace AdventOfCode.Solutions.Year2020.Day15;

class Solution : SolutionBase
{
    List<int> startingNumbers;
    Dictionary<int, int> mostRecentlySpoken = new Dictionary<int, int>();

    public Solution()
        : base(15, 2020, "")
    {
        startingNumbers = new List<int>(Input.Split(',').Select(s => int.Parse(s)));
    }

    protected override string SolvePartOne()
    {
        int turn = 0;

        foreach (int startingNumber in startingNumbers)
        {
            turn++;
            mostRecentlySpoken.Add(startingNumber, turn);
        }

        int current = startingNumbers.Last();
        while (turn < 2020)
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

        return current.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
