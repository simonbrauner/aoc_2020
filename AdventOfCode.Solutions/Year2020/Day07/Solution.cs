namespace AdventOfCode.Solutions.Year2020.Day07;

using System.Text.RegularExpressions;

record BagContent(int count, string description);

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
        HashSet<string> canContainShinyGold = new HashSet<string>();

        foreach (string bag in bagContents.Keys)
        {
            TryIncludeShinyGold(bag, ref canContainShinyGold);
        }

        return canContainShinyGold.Count().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    void TryIncludeShinyGold(string bag, ref HashSet<string> canContainShinyGold)
    {
        Queue<string> queue = new Queue<string>(bagContents[bag].Select(c => c.description));

        string? current;
        while (queue.TryDequeue(out current))
        {
            if (current == ShinyGoldName || canContainShinyGold.Contains(current))
            {
                canContainShinyGold.Add(bag);
                return;
            }

            foreach (BagContent bagContent in bagContents[current])
            {
                queue.Enqueue(bagContent.description);
            }
        }
    }
}
