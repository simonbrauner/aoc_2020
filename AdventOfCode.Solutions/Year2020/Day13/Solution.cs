namespace AdventOfCode.Solutions.Year2020.Day13;

using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    int earliestTimestamp;
    List<int> departures = new List<int>();

    public Solution()
        : base(13, 2020, "")
    {
        string[] lines = Input.SplitByNewline();

        earliestTimestamp = int.Parse(lines[0]);
        foreach (Match match in Regex.Matches(lines[1], @"\d+"))
        {
            departures.Add(int.Parse(match.Value));
        }
    }

    protected override string SolvePartOne()
    {
        int minBus = departures[0];
        int minWaiting = WaitingTime(departures[0]);

        foreach (int departure in departures)
        {
            if (WaitingTime(departure) < minWaiting)
            {
                minBus = departure;
                minWaiting = WaitingTime(departure);
            }
        }

        return (minBus * minWaiting).ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    int WaitingTime(int departure)
    {
        return departure - (earliestTimestamp % departure);
    }
}
