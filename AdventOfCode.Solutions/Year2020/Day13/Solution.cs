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
        foreach (Match match in Regex.Matches(lines[1], @"\d+|x"))
        {
            if (match.Value == "x")
            {
                departures.Add(0);
            }
            else
            {
                departures.Add(int.Parse(match.Value));
            }
        }
    }

    protected override string SolvePartOne()
    {
        int minBus = departures[0];
        int minWaiting = WaitingTime(departures[0]);

        foreach (int departure in departures)
        {
            if (departure != 0 && WaitingTime(departure) < minWaiting)
            {
                minBus = departure;
                minWaiting = WaitingTime(departure);
            }
        }

        return (minBus * minWaiting).ToString();
    }

    protected override string SolvePartTwo()
    {
        // Chinese remainder theorem

        for (int i = 0; i < departures.Count(); i++)
        {
            int departure = departures[i];

            if (departure != 0)
            {
                // Console.WriteLine($"x ≡ {i} (mod {departure})");
            }
        }

        // x ≡ a (mod b) => result = b - a

        return "not computed directly";
    }

    int WaitingTime(int departure)
    {
        return departure - (earliestTimestamp % departure);
    }
}
