namespace AdventOfCode.Solutions.Year2020.Day19;

using System.Text.RegularExpressions;

abstract record RuleValue()
{
    public abstract HashSet<string> ValidStrings(Dictionary<int, RuleValue> rules);
}

record LetterRuleValue(char letter) : RuleValue()
{
    public override HashSet<string> ValidStrings(Dictionary<int, RuleValue> rules)
    {
        return new HashSet<string>() { letter.ToString() };
    }
}

record NumberRuleValue(List<int[]> alternatives) : RuleValue()
{
    public override HashSet<string> ValidStrings(Dictionary<int, RuleValue> rules)
    {
        HashSet<string> results = new HashSet<string>();

        foreach (int[] alternative in alternatives)
        {
            HashSet<string> current = rules[alternative[0]].ValidStrings(rules);

            foreach (int number in alternative.Skip(1))
            {
                HashSet<string> next = rules[number].ValidStrings(rules);
                current = new HashSet<string>(from a in current from b in next select a + b);
            }

            results.UnionWith(current);
        }

        return results;
    }
}

class Solution : SolutionBase
{
    Dictionary<int, RuleValue> rules = new Dictionary<int, RuleValue>();
    List<string> messages;

    public Solution()
        : base(19, 2020, "")
    {
        string[] paragraphs = Input.SplitByParagraph();

        foreach (string line in paragraphs[0].SplitByNewline())
        {
            string[] parts = line.Split(':');
            int ruleNumber = int.Parse(parts[0]);
            RuleValue ruleValue;

            if (parts[1].Contains('"'))
            {
                ruleValue = new LetterRuleValue(Regex.Match(parts[1], @"\w+").Value[0]);
            }
            else
            {
                ruleValue = ParseRuleAlternatives(parts[1]);
            }

            rules.Add(ruleNumber, ruleValue);
        }

        messages = new List<string>(paragraphs[1].SplitByNewline());
    }

    protected override string SolvePartOne()
    {
        return rules[0].ValidStrings(rules).Intersect(messages).Count().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    RuleValue ParseRuleAlternatives(string textRepresentation)
    {
        List<int[]> ruleAlternatives = new List<int[]>();

        foreach (string alternative in textRepresentation.Split('|'))
        {
            int[] ruleAlternative = Regex
                .Matches(alternative, @"\d+")
                .Select(m => int.Parse(m.Value))
                .ToArray();
            ruleAlternatives.Add(ruleAlternative);
        }

        return new NumberRuleValue(ruleAlternatives);
    }
}
