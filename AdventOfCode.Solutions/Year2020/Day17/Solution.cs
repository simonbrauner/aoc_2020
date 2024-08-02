namespace AdventOfCode.Solutions.Year2020.Day17;

record Coordinates(int X, int Y, int Z, int W);

class CubeGrid
{
    HashSet<Coordinates> currentCubes = new HashSet<Coordinates>();
    HashSet<Coordinates> nextCubes = new HashSet<Coordinates>();
    public int[] Boundaries { get; private set; } = { 0, 0, 0, 0, 0, 0, 0, 0 };
    Func<int[], Coordinates, int>[] updateBoundaries =
    {
        (b, c) => c.X < b[0] ? c.X : b[0],
        (b, c) => c.X > b[1] ? c.X : b[1],
        (b, c) => c.Y < b[2] ? c.Y : b[2],
        (b, c) => c.Y > b[3] ? c.Y : b[3],
        (b, c) => c.Z < b[4] ? c.Z : b[4],
        (b, c) => c.Z > b[5] ? c.Z : b[5],
        (b, c) => c.W < b[6] ? c.W : b[6],
        (b, c) => c.W > b[7] ? c.W : b[7]
    };

    public CubeGrid(string textRepresentation)
    {
        int y = 0;
        foreach (string line in textRepresentation.SplitByNewline())
        {
            int x = 0;
            foreach (char letter in line)
            {
                if (letter == '#')
                {
                    ActivateCube(x, y, 0, 0);
                }

                x++;
            }

            y++;
        }

        Update();
    }

    public void ActivateCube(int x, int y, int z, int w)
    {
        Coordinates coordinates = new(x, y, z, w);

        nextCubes.Add(coordinates);

        for (int i = 0; i < Boundaries.Count(); i++)
        {
            Boundaries[i] = updateBoundaries[i](Boundaries, coordinates);
        }
    }

    public void DeactivateCube(int x, int y, int z, int w)
    {
        nextCubes.Remove(new(x, y, z, w));
    }

    public bool IsCubeActive(int x, int y, int z, int w)
    {
        return currentCubes.Contains(new(x, y, z, w));
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

class CubeGrid3D : CubeGrid
{
    public CubeGrid3D(string textRepresentation)
        : base(textRepresentation) { }

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
                        if (IsCubeActive(x + dx, y + dy, z + dz, 0))
                        {
                            active++;
                        }
                    }
                }
            }
        }

        return active;
    }

    public int Solve()
    {
        for (int i = 0; i < 6; i++)
        {
            int[] b = Boundaries;
            for (int x = b[0] - 1; x <= b[1] + 1; x++)
            {
                for (int y = b[2] - 1; y <= b[3] + 1; y++)
                {
                    for (int z = b[4] - 1; z <= b[5] + 1; z++)
                    {
                        bool active = IsCubeActive(x, y, z, 0);
                        int activeNeighbors = CountActiveNeighbors(x, y, z);

                        if (active && !(activeNeighbors == 2 || activeNeighbors == 3))
                        {
                            DeactivateCube(x, y, z, 0);
                        }
                        else if (!active && activeNeighbors == 3)
                        {
                            ActivateCube(x, y, z, 0);
                        }
                    }
                }
            }

            Update();
        }

        return CountActiveCubes();
    }
}

class CubeGrid4D : CubeGrid
{
    public CubeGrid4D(string textRepresentation)
        : base(textRepresentation) { }

    public int CountActiveNeighbors(int x, int y, int z, int w)
    {
        int active = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    for (int dw = -1; dw <= 1; dw++)
                    {
                        if (dx != 0 || dy != 0 || dz != 0 || dw != 0)
                        {
                            if (IsCubeActive(x + dx, y + dy, z + dz, w + dw))
                            {
                                active++;
                            }
                        }
                    }
                }
            }
        }

        return active;
    }

    public int Solve()
    {
        for (int i = 0; i < 6; i++)
        {
            int[] b = Boundaries;
            for (int x = b[0] - 1; x <= b[1] + 1; x++)
            {
                for (int y = b[2] - 1; y <= b[3] + 1; y++)
                {
                    for (int z = b[4] - 1; z <= b[5] + 1; z++)
                    {
                        for (int w = b[6] - 1; w <= b[7] + 1; w++)
                        {
                            bool active = IsCubeActive(x, y, z, w);
                            int activeNeighbors = CountActiveNeighbors(x, y, z, w);

                            if (active && !(activeNeighbors == 2 || activeNeighbors == 3))
                            {
                                DeactivateCube(x, y, z, w);
                            }
                            else if (!active && activeNeighbors == 3)
                            {
                                ActivateCube(x, y, z, w);
                            }
                        }
                    }
                }
            }

            Update();
        }

        return CountActiveCubes();
    }
}

class Solution : SolutionBase
{
    public Solution()
        : base(17, 2020, "") { }

    protected override string SolvePartOne()
    {
        return (new CubeGrid3D(Input)).Solve().ToString();
    }

    protected override string SolvePartTwo()
    {
        return (new CubeGrid4D(Input)).Solve().ToString();
    }
}
