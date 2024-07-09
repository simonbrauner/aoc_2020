namespace AdventOfCode.Solutions.Year2020.Day07;

using System.Text.RegularExpressions;

record BagContent(int Count, string Description);

class Solution : SolutionBase
{
    const string ShinyGoldName = "shiny gold";
    Dictionary<string, List<BagContent>> bagContents = new Dictionary<string, List<BagContent>>();

    public Solution()
        : base(07, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            string bag = Regex.Match(line, @"^(.*?) bags").Groups[1].Value;

            List<BagContent> contents = new List<BagContent>();
            foreach (Match match in Regex.Matches(line, @"(\d+) (.*?) bag"))
            {
                contents.Add(new(int.Parse(match.Groups[1].Value), match.Groups[2].Value));
            }

            bagContents.Add(bag, contents);
        }
    }

    protected override string SolvePartOne()
    {
        return bagContents.Keys.Count(b => CanIncludeShinyGold(b)).ToString();
    }

    protected override string SolvePartTwo()
    {
        return CountBags(ShinyGoldName).ToString();
    }

    bool CanIncludeShinyGold(string bag)
    {
        Queue<string> queue = new Queue<string>(bagContents[bag].Select(c => c.Description));

        string? current;
        while (queue.TryDequeue(out current))
        {
            if (current == ShinyGoldName)
            {
                return true;
            }

            foreach (BagContent bagContent in bagContents[current])
            {
                queue.Enqueue(bagContent.Description);
            }
        }

        return false;
    }

    int CountBags(string bag)
    {
        int count = 0;

        foreach (BagContent bagContent in bagContents[bag])
        {
            count += bagContent.Count * (CountBags(bagContent.Description) + 1);
        }

        return count;
    }
}
