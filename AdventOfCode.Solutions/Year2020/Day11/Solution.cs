namespace AdventOfCode.Solutions.Year2020.Day11;

class SeatGrid
{
    List<StringBuilder> currentGrid = new List<StringBuilder>();
    List<StringBuilder> nextGrid = new List<StringBuilder>();

    public SeatGrid(string[] lines)
    {
        foreach (string line in lines)
        {
            currentGrid.Add(new StringBuilder(line));
            nextGrid.Add(new StringBuilder(line));
        }
    }

    public int RowCount()
    {
        return currentGrid.Count();
    }

    public int ColCount()
    {
        return currentGrid[0].Length;
    }

    public bool IsOccupiedSeat(int x, int y)
    {
        return currentGrid[y][x] == '#';
    }

    public bool IsEmptySeat(int x, int y)
    {
        return currentGrid[y][x] == 'L';
    }

    bool RowValid(int y)
    {
        return y >= 0 && y < RowCount();
    }

    bool ColValid(int x)
    {
        return x >= 0 && x < ColCount();
    }

    public void AdjacentCounts(int x, int y, out int emptyCount, out int occupiedCount)
    {
        emptyCount = 0;
        occupiedCount = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && RowValid(y + dy) && ColValid(x + dx))
                {
                    if (IsEmptySeat(x + dx, y + dy))
                    {
                        emptyCount++;
                    }
                    else if (IsOccupiedSeat(x + dx, y + dy))
                    {
                        occupiedCount++;
                    }
                }
            }
        }
    }

    public void OccupySeat(int x, int y)
    {
        nextGrid[y][x] = '#';
    }

    public void EmptySeat(int x, int y)
    {
        nextGrid[y][x] = 'L';
    }

    public int OccupiedCount()
    {
        int occupiedCount = 0;

        for (int x = 0; x < ColCount(); x++)
        {
            for (int y = 0; y < RowCount(); y++)
            {
                if (IsOccupiedSeat(x, y))
                {
                    occupiedCount++;
                }
            }
        }

        return occupiedCount;
    }

    public bool Converged()
    {
        for (int x = 0; x < ColCount(); x++)
        {
            for (int y = 0; y < RowCount(); y++)
            {
                if (currentGrid[y][x] != nextGrid[y][x])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void Update()
    {
        for (int x = 0; x < ColCount(); x++)
        {
            for (int y = 0; y < RowCount(); y++)
            {
                currentGrid[y][x] = nextGrid[y][x];
            }
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (StringBuilder line in currentGrid)
        {
            stringBuilder.Append(line);
            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}

class Solution : SolutionBase
{
    SeatGrid seatGrid;

    public Solution()
        : base(11, 2020, "")
    {
        seatGrid = new SeatGrid(Input.SplitByNewline());
    }

    protected override string SolvePartOne()
    {
        while (true)
        {
            for (int x = 0; x < seatGrid.ColCount(); x++)
            {
                for (int y = 0; y < seatGrid.RowCount(); y++)
                {
                    int emptyCount;
                    int occupiedCount;
                    seatGrid.AdjacentCounts(x, y, out emptyCount, out occupiedCount);

                    if (seatGrid.IsEmptySeat(x, y) && occupiedCount == 0)
                    {
                        seatGrid.OccupySeat(x, y);
                    }
                    else if (seatGrid.IsOccupiedSeat(x, y) && occupiedCount >= 4)
                    {
                        seatGrid.EmptySeat(x, y);
                    }
                }
            }

            if (seatGrid.Converged())
            {
                return seatGrid.OccupiedCount().ToString();
            }

            seatGrid.Update();
        }
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
