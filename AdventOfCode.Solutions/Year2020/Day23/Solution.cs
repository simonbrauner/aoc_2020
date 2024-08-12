namespace AdventOfCode.Solutions.Year2020.Day23;

class CupCircle
{
    List<int> cups;
    int currentIndex;

    public CupCircle(string stringRepresentation)
    {
        cups = new List<int>(stringRepresentation.Select(c => int.Parse(c.ToString())));
        currentIndex = 0;
    }

    public void Turn()
    {
        List<int> threeCupIndices = new List<int>(
            Enumerable.Range(0, 3).Select(i => NextIndex(currentIndex + i))
        );

        int destinationIndex = DestinationIndex(threeCupIndices);

        for (int i = destinationIndex; i != threeCupIndices[2]; i = PreviousIndex(i))
        {
            int destinationValue = cups[destinationIndex];

            for (int j = destinationIndex; j != threeCupIndices[0]; j = PreviousIndex(j))
            {
                cups[j] = cups[PreviousIndex(j)];
            }

            cups[threeCupIndices[0]] = destinationValue;
        }

        currentIndex = NextIndex(currentIndex);
    }

    int PreviousIndex(int index)
    {
        if (index == 0)
        {
            return cups.Count() - 1;
        }

        return index - 1;
    }

    int NextIndex(int index)
    {
        return (index + 1) % cups.Count();
    }

    int PreviousValue(int value)
    {
        if (value == 1)
        {
            return cups.Count();
        }

        return value - 1;
    }

    int DestinationIndex(List<int> threeCupIndices)
    {
        List<int> threeCupValues = new List<int>(threeCupIndices.Select(i => cups[i]));
        int value = cups[currentIndex];

        while (true)
        {
            value = PreviousValue(value);

            if (!threeCupValues.Contains(value))
            {
                return cups.IndexOf(value);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (int cup in cups)
        {
            stringBuilder.Append(cup);
        }

        return stringBuilder.ToString();
    }

    public string LabelsAfterCup1()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = NextIndex(cups.IndexOf(1)); cups[i] != 1; i = NextIndex(i))
        {
            stringBuilder.Append(cups[i]);
        }

        return stringBuilder.ToString();
    }
}

class Solution : SolutionBase
{
    CupCircle cupCircle;

    public Solution()
        : base(23, 2020, "")
    {
        cupCircle = new CupCircle(Input.Trim());
    }

    protected override string SolvePartOne()
    {
        for (int i = 0; i < 100; i++)
        {
            cupCircle.Turn();
        }

        return cupCircle.LabelsAfterCup1();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
