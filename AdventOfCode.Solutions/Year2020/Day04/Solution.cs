namespace AdventOfCode.Solutions.Year2020.Day04;

using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    readonly string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
    List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

    public Solution()
        : base(04, 2020, "")
    {
        foreach (String sequence in Input.SplitByParagraph())
        {
            Dictionary<string, string> passport = new Dictionary<string, string>();

            foreach (Match match in Regex.Matches(sequence, @"(\S+):(\S+)"))
            {
                passport.Add(match.Groups[1].Value, match.Groups[2].Value);
            }

            passports.Add(passport);
        }
    }

    protected override string SolvePartOne()
    {
        int result = passports.Count;

        foreach (Dictionary<string, string> passport in passports)
        {
            foreach (String requiredField in requiredFields)
            {
                if (!passport.ContainsKey(requiredField))
                {
                    result--;
                    break;
                }
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
