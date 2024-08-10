namespace AdventOfCode.Solutions.Year2020.Day25;

class Solution : SolutionBase
{
    const long remainderOf = 20201227;
    List<long> publicKeys;

    public Solution()
        : base(25, 2020, "")
    {
        publicKeys = new List<long>(Input.SplitByNewline().Select(l => long.Parse(l)));
    }

    protected override string SolvePartOne()
    {
        List<long> loopSizes = publicKeys.Select(guessLoopSize).ToList();

        long value = 1;
        for (int i = 0; i < loopSizes[0]; i++)
        {
            value = TransformIteration(value, publicKeys[1]);
        }

        return value.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    long TransformIteration(long value, long subjectNumber = 7)
    {
        return (value * subjectNumber) % remainderOf;
    }

    long guessLoopSize(long publicKey)
    {
        long loopCount = 0;

        long value = 1;
        while (value != publicKey)
        {
            value = TransformIteration(value);

            loopCount++;
        }

        return loopCount;
    }
}
