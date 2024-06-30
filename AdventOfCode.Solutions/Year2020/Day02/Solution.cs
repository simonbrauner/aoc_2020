namespace AdventOfCode.Solutions.Year2020.Day02;

using System.Text.RegularExpressions;

record Password(int MinLength, int MaxLenght, char Letter, string Value);

class Solution : SolutionBase
{
    List<Password> passwords = new List<Password>();

    public Solution()
        : base(02, 2020, "")
    {
        foreach (
            Match match in Regex.Matches(
                Input,
                @"^(\d+)-(\d+) (\w): (\w+)$",
                RegexOptions.Multiline
            )
        )
        {
            passwords.Add(
                new(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    match.Groups[3].Value[0],
                    match.Groups[4].Value
                )
            );
        }
    }

    protected override string SolvePartOne()
    {
        int result = 0;

        foreach (Password password in passwords)
        {
            int letterCount = password.Value.CountLetters(password.Letter);

            if (letterCount >= password.MinLength && letterCount <= password.MaxLenght)
            {
                result++;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
