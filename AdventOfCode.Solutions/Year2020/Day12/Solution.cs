namespace AdventOfCode.Solutions.Year2020.Day12;

class Ship
{
    readonly Dictionary<int, char> orientationDirection = new Dictionary<int, char>
    {
        { 0, 'N' },
        { 180, 'S' },
        { 90, 'E' },
        { 270, 'W' }
    };
    int orientation = 90;
    int east = 0;
    int north = 0;

    public Ship() { }

    public void Move(string instruction)
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
                orientation = orientation - value;
                break;
            case 'R':
                orientation = orientation + value;
                break;
        }

        if (orientation < 0)
        {
            orientation += 360;
        }
        if (orientation >= 360)
        {
            orientation -= 360;
        }
    }

    public int ManhattanDistanceFromStart()
    {
        return Math.Abs(east) + Math.Abs(north);
    }
}

class Solution : SolutionBase
{
    public Solution()
        : base(12, 2020, "") { }

    protected override string SolvePartOne()
    {
        Ship ship = new Ship();

        foreach (string instruction in Input.SplitByNewline())
        {
            ship.Move(instruction);
        }

        return ship.ManhattanDistanceFromStart().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
