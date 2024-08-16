namespace AdventOfCode.Solutions.Year2020.Day24;

using System.Text.RegularExpressions;

record TilePosition(int North, int East)
{
    static readonly Dictionary<string, TilePosition> NeighborDifferences = new Dictionary<
        string,
        TilePosition
    >
    {
        { "e", new(0, 2) },
        { "se", new(-1, 1) },
        { "sw", new(-1, -1) },
        { "w", new(0, -2) },
        { "nw", new(1, -1) },
        { "ne", new(1, 1) }
    };

    public static TilePosition Create(string stringRepresentation)
    {
        TilePosition tilePosition = new(0, 0);

        foreach (Match match in Regex.Matches(stringRepresentation, @"(e|se|sw|w|nw|ne)"))
        {
            tilePosition += NeighborDifferences[match.Value];
        }

        return tilePosition;
    }

    public int BlackNeighborCount(HashSet<TilePosition> blackTiles)
    {
        int count = 0;

        foreach (TilePosition neighborDifference in NeighborDifferences.Values)
        {
            if (blackTiles.Contains(this + neighborDifference))
            {
                count++;
            }
        }

        return count;
    }

    public static TilePosition operator +(TilePosition a, TilePosition b)
    {
        return new(a.North + b.North, a.East + b.East);
    }
}

class Solution : SolutionBase
{
    List<TilePosition> tilePositions;
    HashSet<TilePosition> blackTiles = new HashSet<TilePosition>();

    public Solution()
        : base(24, 2020, "")
    {
        tilePositions = new List<TilePosition>(Input.SplitByNewline().Select(TilePosition.Create));
    }

    protected override string SolvePartOne()
    {
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
        int maxNorth = blackTiles.Select(t => t.North).Max();
        int minNorth = blackTiles.Select(t => t.North).Min();
        int maxEast = blackTiles.Select(t => t.East).Max();
        int minEast = blackTiles.Select(t => t.East).Min();

        for (int dayNumber = 1; dayNumber <= 100; dayNumber++)
        {
            HashSet<TilePosition> newBlackTiles = new HashSet<TilePosition>(blackTiles);

            for (int north = minNorth - 2 * dayNumber; north <= maxNorth + 2 * dayNumber; north++)
            {
                for (int east = minEast - 2 * dayNumber; east <= maxEast + 2 * dayNumber; east++)
                {
                    TilePosition tilePosition = new(north, east);

                    int blackNeighborCount = tilePosition.BlackNeighborCount(blackTiles);

                    if (
                        blackNeighborCount != 1
                        && blackNeighborCount != 2
                        && blackTiles.Contains(tilePosition)
                    )
                    {
                        newBlackTiles.Remove(tilePosition);
                    }

                    if (blackNeighborCount == 2 && !blackTiles.Contains(tilePosition))
                    {
                        newBlackTiles.Add(tilePosition);
                    }
                }
            }

            blackTiles = newBlackTiles;
        }

        return blackTiles.Count().ToString();
    }
}
