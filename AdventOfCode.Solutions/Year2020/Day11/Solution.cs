namespace AdventOfCode.Solutions.Year2020.Day11;

class SeatGrid
{
    List<StringBuilder> currentGrid = new List<StringBuilder>();
    List<StringBuilder> nextGrid = new List<StringBuilder>();
    int part;

    public SeatGrid(string[] lines, int part)
    {
        foreach (string line in lines)
        {
            currentGrid.Add(new StringBuilder(line));
            nextGrid.Add(new StringBuilder(line));
        }

        this.part = part;
    }

    int RowCount()
    {
        return currentGrid.Count();
    }

    int ColCount()
    {
        return currentGrid[0].Length;
    }

    bool IsOccupiedSeat(int x, int y)
    {
        return currentGrid[y][x] == '#';
    }

    bool IsEmptySeat(int x, int y)
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

    bool IsAdjacentOccupied(int x, int y, int dx, int dy)
    {
        if (part == 1)
        {
            return RowValid(y + dy) && ColValid(x + dx) && IsOccupiedSeat(x + dx, y + dy);
        }

        while (true)
        {
            x += dx;
            y += dy;

            if (!RowValid(y) || !ColValid(x) || IsEmptySeat(x, y))
            {
                return false;
            }

            if (IsOccupiedSeat(x, y))
            {
                return true;
            }
        }
    }

    int AdjacentOccupiedCount(int x, int y)
    {
        int occupiedCount = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && IsAdjacentOccupied(x, y, dx, dy))
                {
                    occupiedCount++;
                }
            }
        }

        return occupiedCount;
    }

    void OccupySeat(int x, int y)
    {
        nextGrid[y][x] = '#';
    }

    void EmptySeat(int x, int y)
    {
        nextGrid[y][x] = 'L';
    }

    int OccupiedCount()
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

    bool Converged()
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

    void Update()
    {
        for (int x = 0; x < ColCount(); x++)
        {
            for (int y = 0; y < RowCount(); y++)
            {
                currentGrid[y][x] = nextGrid[y][x];
            }
        }
    }

    bool SeatBecomesEmpty(int occupiedCount)
    {
        return (part == 1 && occupiedCount >= 4) || (part == 2 && occupiedCount >= 5);
    }

    public int Solve()
    {
        while (true)
        {
            for (int x = 0; x < ColCount(); x++)
            {
                for (int y = 0; y < RowCount(); y++)
                {
                    int occupiedCount = AdjacentOccupiedCount(x, y);

                    if (IsEmptySeat(x, y) && occupiedCount == 0)
                    {
                        OccupySeat(x, y);
                    }
                    else if (IsOccupiedSeat(x, y) && SeatBecomesEmpty(occupiedCount))
                    {
                        EmptySeat(x, y);
                    }
                }
            }

            if (Converged())
            {
                return OccupiedCount();
            }

            Update();
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
    public Solution()
        : base(11, 2020, "") { }

    protected override string SolvePartOne()
    {
        return new SeatGrid(Input.SplitByNewline(), 1).Solve().ToString();
    }

    protected override string SolvePartTwo()
    {
        return new SeatGrid(Input.SplitByNewline(), 2).Solve().ToString();
    }
}
