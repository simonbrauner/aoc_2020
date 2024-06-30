namespace AdventOfCode.Solutions.Year2020.Day02;

using System.Text.RegularExpressions;

record Password(int FirstNumber, int SecondNumber, char Letter, string Value);

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
            int minLength = password.FirstNumber;
            int maxLength = password.SecondNumber;

            if (letterCount >= minLength && letterCount <= maxLength)
            {
                result++;
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        int result = 0;

        foreach (Password password in passwords)
        {
            int matchCount = 0;
            int firstIndex = password.FirstNumber - 1;
            int secondIndex = password.SecondNumber - 1;

            if (password.Value[firstIndex] == password.Letter)
            {
                matchCount++;
            }
            if (password.Value[secondIndex] == password.Letter)
            {
                matchCount++;
            }

            if (matchCount == 1)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
