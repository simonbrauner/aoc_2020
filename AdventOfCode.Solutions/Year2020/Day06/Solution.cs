namespace AdventOfCode.Solutions.Year2020.Day06;

class Solution : SolutionBase
{
    List<List<string>> answersByGroups = new List<List<string>>();

    public Solution()
        : base(06, 2020, "")
    {
        foreach (string answerGroup in Input.SplitByParagraph())
        {
            List<string> groupAnswers = new List<String>(answerGroup.SplitByNewline());
            answersByGroups.Add(groupAnswers);
        }
    }

    protected override string SolvePartOne()
    {
        int answerSum = 0;

        foreach (List<string> groupAnswers in answersByGroups)
        {
            HashSet<char> anyAnsweredYes = new HashSet<char>();

            foreach (string yesAnswers in groupAnswers)
            {
                anyAnsweredYes.UnionWith(yesAnswers);
            }

            answerSum += anyAnsweredYes.Count();
        }

        return answerSum.ToString();
    }

    protected override string SolvePartTwo()
    {
        int answerSum = 0;

        foreach (List<string> groupAnswers in answersByGroups)
        {
            HashSet<char> allAnsweredYes = new HashSet<char>(groupAnswers[0]);

            foreach (string yesAnswers in groupAnswers.Skip(1))
            {
                allAnsweredYes.IntersectWith(yesAnswers);
            }

            answerSum += allAnsweredYes.Count();
        }

        return answerSum.ToString();
    }
}
