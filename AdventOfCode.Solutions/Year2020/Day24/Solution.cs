namespace AdventOfCode.Solutions.Year2020.Day24;

using System.Text.RegularExpressions;

record TilePosition(int North, int East)
{
    public static TilePosition Create(string stringRepresentation)
    {
        int north = 0;
        int east = 0;

        foreach (Match match in Regex.Matches(stringRepresentation, @"(e|se|sw|w|nw|ne)"))
        {
            switch (match.Value)
            {
                case "e":
                    east += 2;
                    break;
                case "se":
                    north--;
                    east++;
                    break;
                case "sw":
                    north--;
                    east--;
                    break;
                case "w":
                    east -= 2;
                    break;
                case "nw":
                    north++;
                    east--;
                    break;
                case "ne":
                    north++;
                    east++;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        return new(north, east);
    }
}

class Solution : SolutionBase
{
    List<TilePosition> tilePositions;

    public Solution()
        : base(24, 2020, "")
    {
        tilePositions = new List<TilePosition>(Input.SplitByNewline().Select(TilePosition.Create));
    }

    protected override string SolvePartOne()
    {
        HashSet<TilePosition> blackTiles = new HashSet<TilePosition>();

        foreach (TilePosition tilePosition in tilePositions)
        {
            if (!blackTiles.Remove(tilePosition))
            {
                blackTiles.Add(tilePosition);
            }
        }

        return blackTiles.Count().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
