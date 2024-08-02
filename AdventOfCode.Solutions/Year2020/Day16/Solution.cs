namespace AdventOfCode.Solutions.Year2020.Day16;

using System.Text.RegularExpressions;

record Range(int FirstMin, int FirstMax, int SecondMin, int SecondMax)
{
    public bool InRange(int number)
    {
        return (number >= FirstMin && number <= FirstMax)
            || (number >= SecondMin && number <= SecondMax);
    }
}

class Solution : SolutionBase
{
    Dictionary<string, Range> fields = new Dictionary<string, Range>();
    int[] myTicket;
    List<int[]> nearbyTickets = new List<int[]>();

    public Solution()
        : base(16, 2020, "")
    {
        string[] paragraphs = Input.SplitByParagraph();

        foreach (Match match in Regex.Matches(paragraphs[0], @"(.+): (\d+)-(\d+) or (\d+)-(\d+)"))
        {
            int[] numeric = match
                .Groups.Cast<Group>()
                .Skip(2)
                .Select(g => int.Parse(g.Value))
                .ToArray();

            fields.Add(match.Groups[1].Value, new(numeric[0], numeric[1], numeric[2], numeric[3]));
        }

        myTicket = ArrayTicket(paragraphs[1].SplitByNewline()[1]);

        foreach (string ticketLine in paragraphs[2].SplitByNewline().Skip(1))
        {
            nearbyTickets.Add(ArrayTicket(ticketLine));
        }
    }

    protected override string SolvePartOne()
    {
        int errorRate = 0;

        foreach (int[] ticket in nearbyTickets)
        {
            foreach (int invalidIndex in InvalidIndices(ticket))
            {
                errorRate += ticket[invalidIndex];
            }
        }

        return errorRate.ToString();
    }

    protected override string SolvePartTwo()
    {
        long departureFieldsProduct = 1;
        Dictionary<string, bool[]> fieldValidIndices = FieldValidIndices();
        Dictionary<string, int> fieldIndices = new Dictionary<string, int>();

        while (fieldIndices.Count() != fields.Count())
        {
            foreach (string name in fieldValidIndices.Keys)
            {
                bool[] valid = fieldValidIndices[name];

                if (valid.Count(i => i == true) == 1)
                {
                    int index = Array.IndexOf(valid, true);
                    fieldIndices.Add(name, index);

                    foreach (string innerName in fieldValidIndices.Keys)
                    {
                        fieldValidIndices[innerName][index] = false;
                    }
                }
            }
        }

        foreach ((string name, int index) in fieldIndices)
        {
            if (name.StartsWith("departure"))
            {
                departureFieldsProduct *= myTicket[index];
            }
        }

        return departureFieldsProduct.ToString();
    }

    int[] ArrayTicket(string stringTicket)
    {
        return stringTicket.Split(',').Select(s => int.Parse(s)).ToArray();
    }

    List<int> InvalidIndices(int[] ticket)
    {
        List<int> invalidIndices = new List<int>();

        for (int i = 0; i < ticket.Count(); i++)
        {
            bool valid = false;

            foreach (Range range in fields.Values)
            {
                if (range.InRange(ticket[i]))
                {
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                invalidIndices.Add(i);
            }
        }

        return invalidIndices;
    }

    Dictionary<string, bool[]> FieldValidIndices()
    {
        Dictionary<string, bool[]> fieldValidIndices = new Dictionary<string, bool[]>();

        foreach ((string name, Range range) in fields)
        {
            bool[] valid = nearbyTickets[0].Select(_ => true).ToArray();

            foreach (int[] ticket in nearbyTickets.Where(t => InvalidIndices(t).Count() == 0))
            {
                for (int i = 0; i < ticket.Count(); i++)
                {
                    if (!range.InRange(ticket[i]))
                    {
                        valid[i] = false;
                    }
                }
            }

            fieldValidIndices.Add(name, valid);
        }

        return fieldValidIndices;
    }
}
