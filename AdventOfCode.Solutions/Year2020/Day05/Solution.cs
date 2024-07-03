namespace AdventOfCode.Solutions.Year2020.Day05;

class Seat
{
    const int colNumberOffset = 7;
    const char rowAddLetter = 'B';
    const char colAddLetter = 'R';
    int rowNumber = 0;
    int colNumber = 0;

    public Seat(string textRepresentation)
    {
        int power = 1;
        for (int i = colNumberOffset - 1; i >= 0; i--)
        {
            if (textRepresentation[i] == rowAddLetter)
            {
                rowNumber += power;
            }

            power *= 2;
        }

        power = 1;
        for (int i = textRepresentation.Length - 1; i >= colNumberOffset; i--)
        {
            if (textRepresentation[i] == colAddLetter)
            {
                colNumber += power;
            }

            power *= 2;
        }
    }

    public int Id()
    {
        return rowNumber * 8 + colNumber;
    }
}

class Solution : SolutionBase
{
    List<Seat> seats = new List<Seat>();

    public Solution()
        : base(05, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            seats.Add(new Seat(line));
        }
    }

    protected override string SolvePartOne()
    {
        return seats.Max(s => s.Id()).ToString();
    }

    protected override string SolvePartTwo()
    {
        HashSet<int> ids = new HashSet<int>(seats.Select(s => s.Id()));

        foreach (int id in ids)
        {
            int myId = id + 1;

            if (ids.Contains(id + 2) && !ids.Contains(myId))
            {
                return myId.ToString();
            }
        }

        return "";
    }
}
