namespace AdventOfCode.Solutions.Year2020.Day17;

record Coordinates(int X, int Y, int Z);

class CubeGrid
{
    HashSet<Coordinates> currentCubes = new HashSet<Coordinates>();
    HashSet<Coordinates> nextCubes = new HashSet<Coordinates>();
    public int[] Boundaries { get; private set; } = { 0, 0, 0, 0, 0, 0 };
    Func<int[], Coordinates, int>[] updateBoundaries =
    {
        (b, c) => c.X < b[0] ? c.X : b[0],
        (b, c) => c.X > b[1] ? c.X : b[1],
        (b, c) => c.Y < b[2] ? c.Y : b[2],
        (b, c) => c.Y > b[3] ? c.Y : b[3],
        (b, c) => c.Z < b[4] ? c.Z : b[4],
        (b, c) => c.Z > b[5] ? c.Z : b[5]
    };

    public void ActivateCube(int x, int y, int z)
    {
        Coordinates coordinates = new(x, y, z);

        nextCubes.Add(coordinates);

        for (int i = 0; i < Boundaries.Count(); i++)
        {
            Boundaries[i] = updateBoundaries[i](Boundaries, coordinates);
        }
    }

    public void DeactivateCube(int x, int y, int z)
    {
        nextCubes.Remove(new(x, y, z));
    }

    public bool IsCubeActive(int x, int y, int z)
    {
        return currentCubes.Contains(new(x, y, z));
    }

    public int CountActiveNeighbors(int x, int y, int z)
    {
        int active = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    if (dx != 0 || dy != 0 || dz != 0)
                    {
                        if (IsCubeActive(x + dx, y + dy, z + dz))
                        {
                            active++;
                        }
                    }
                }
            }
        }

        return active;
    }

    public void Update()
    {
        currentCubes = new HashSet<Coordinates>(nextCubes);
    }

    public int CountActiveCubes()
    {
        return currentCubes.Count();
    }
}

class Solution : SolutionBase
{
    CubeGrid cubeGrid;

    public Solution()
        : base(17, 2020, "")
    {
        cubeGrid = new CubeGrid();

        int y = 0;
        foreach (string line in Input.SplitByNewline())
        {
            int x = 0;
            foreach (char letter in line)
            {
                if (letter == '#')
                {
                    cubeGrid.ActivateCube(x, y, 0);
                }

                x++;
            }

            y++;
        }

        cubeGrid.Update();
    }

    protected override string SolvePartOne()
    {
        for (int i = 0; i < 6; i++)
        {
            int[] b = cubeGrid.Boundaries;
            for (int x = b[0] - 1; x <= b[1] + 1; x++)
            {
                for (int y = b[2] - 1; y <= b[3] + 1; y++)
                {
                    for (int z = b[4] - 1; z <= b[5] + 1; z++)
                    {
                        bool active = cubeGrid.IsCubeActive(x, y, z);
                        int activeNeighbors = cubeGrid.CountActiveNeighbors(x, y, z);

                        if (active && !(activeNeighbors == 2 || activeNeighbors == 3))
                        {
                            cubeGrid.DeactivateCube(x, y, z);
                        }
                        else if (!active && activeNeighbors == 3)
                        {
                            cubeGrid.ActivateCube(x, y, z);
                        }
                    }
                }
            }

            cubeGrid.Update();
        }

        return cubeGrid.CountActiveCubes().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
