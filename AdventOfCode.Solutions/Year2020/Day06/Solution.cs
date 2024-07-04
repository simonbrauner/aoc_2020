namespace AdventOfCode.Solutions.Year2020.Day06;

class Solution : SolutionBase
{
    List<HashSet<char>> yesAnswersByGroups = new List<HashSet<char>>();

    public Solution()
        : base(06, 2020, "")
    {
        foreach (string yesAnswersGroup in Input.SplitByParagraph())
        {
            HashSet<char> yesAnswers = new HashSet<char>();

            foreach (char letter in yesAnswersGroup)
            {
                if (Char.IsAsciiLetterLower(letter))
                {
                    yesAnswers.Add(letter);
                }
            }

            yesAnswersByGroups.Add(yesAnswers);
        }
    }

    protected override string SolvePartOne()
    {
        return yesAnswersByGroups.Select(a => a.Count()).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
