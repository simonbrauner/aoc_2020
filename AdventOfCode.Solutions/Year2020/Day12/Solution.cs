namespace AdventOfCode.Solutions.Year2020.Day12;

abstract class Ship
{
    protected int east = 0;
    protected int north = 0;

    protected abstract void Move(string instruction);

    protected int ManhattanDistanceFromStart()
    {
        return Math.Abs(east) + Math.Abs(north);
    }

    public string Solve(string[] instructions)
    {
        foreach (string instruction in instructions)
        {
            Move(instruction);
        }

        return ManhattanDistanceFromStart().ToString();
    }
}

class ShipPartOne : Ship
{
    readonly Dictionary<int, char> orientationDirection = new Dictionary<int, char>
    {
        { 0, 'N' },
        { 180, 'S' },
        { 90, 'E' },
        { 270, 'W' }
    };
    int orientation = 90;

    protected override void Move(string instruction)
    {
        int value = int.Parse(instruction.Substring(1));
        char action = instruction[0];
        if (action == 'F')
        {
            action = orientationDirection[orientation];
        }

        switch (action)
        {
            case 'N':
                north += value;
                break;
            case 'S':
                north -= value;
                break;
            case 'E':
                east += value;
                break;
            case 'W':
                east -= value;
                break;
            case 'L':
                orientation = (orientation - value + 360) % 360;
                break;
            case 'R':
                orientation = (orientation + value + 360) % 360;
                break;
        }
    }
}

class ShipPartTwo : Ship
{
    int waypointEast = 10;
    int waypointNorth = 1;

    protected override void Move(string instruction)
    {
        int value = int.Parse(instruction.Substring(1));
        char action = instruction[0];

        switch (action)
        {
            case 'N':
                waypointNorth += value;
                break;
            case 'S':
                waypointNorth -= value;
                break;
            case 'E':
                waypointEast += value;
                break;
            case 'W':
                waypointEast -= value;
                break;
            case 'L':
                for (int i = 0; i < value; i += 90)
                {
                    (waypointEast, waypointNorth) = (-waypointNorth, waypointEast);
                }
                break;
            case 'R':
                for (int i = 0; i < value; i += 90)
                {
                    (waypointEast, waypointNorth) = (waypointNorth, -waypointEast);
                }
                break;
            case 'F':
                east += value * waypointEast;
                north += value * waypointNorth;
                break;
        }
    }
}

class Solution : SolutionBase
{
    public Solution()
        : base(12, 2020, "") { }

    protected override string SolvePartOne()
    {
        return (new ShipPartOne()).Solve(Input.SplitByNewline());
    }

    protected override string SolvePartTwo()
    {
        return (new ShipPartTwo()).Solve(Input.SplitByNewline());
    }
}
