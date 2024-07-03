namespace AdventOfCode.Solutions.Year2020.Day04;

using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    readonly Dictionary<string, string> requiredFields = new Dictionary<string, string>
    {
        { "byr", @"^(19[2-9]\d|200[0-2])$" },
        { "iyr", @"^(201\d|2020)$" },
        { "eyr", @"^(202\d|2030)$" },
        { "hgt", @"^(((1[5-8]\d|19[0-3])cm)|((59|6\d|7[0-6])in))$" },
        { "hcl", @"^#[0-9a-f]{6}$" },
        { "ecl", @"^(amb|blu|brn|gry|grn|hzl|oth)$" },
        { "pid", @"^\d{9}$" }
    };
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
            foreach (String requiredField in requiredFields.Keys)
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
        int result = passports.Count;

        foreach (Dictionary<string, string> passport in passports)
        {
            foreach ((string requiredField, string regex) in requiredFields)
            {
                string fieldValue = passport.GetValueOrDefault(requiredField, "");

                if (!Regex.IsMatch(fieldValue, regex))
                {
                    result--;
                    break;
                }
            }
        }

        return result.ToString();
    }
}
