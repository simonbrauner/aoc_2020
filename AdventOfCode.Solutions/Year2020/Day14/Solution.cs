namespace AdventOfCode.Solutions.Year2020.Day14;

using System.Text.RegularExpressions;

record Instruction();

record UpdateBitmask(string Mask) : Instruction;

record WriteValue(long Address, long Value) : Instruction;

class Solution : SolutionBase
{
    string mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
    Dictionary<long, long> memory = new Dictionary<long, long>();
    List<Instruction> instructions = new List<Instruction>();

    public Solution()
        : base(14, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            Match match = Regex.Match(line, @"^mask = ([01X]{36})$");
            if (match.Success)
            {
                instructions.Add(new UpdateBitmask(match.Groups[1].Value));
                continue;
            }

            match = Regex.Match(line, @"^mem\[(\d+)\] = (\d+)$");
            instructions.Add(
                new WriteValue(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value))
            );
        }
    }

    protected override string SolvePartOne()
    {
        foreach (Instruction instruction in instructions)
        {
            switch (instruction)
            {
                case UpdateBitmask updateBitmask:
                    mask = updateBitmask.Mask;
                    break;
                case WriteValue writeValue:
                    memory[writeValue.Address] = ComputeValueWithMask(writeValue.Value, mask);
                    break;
            }
        }

        return SumOfValuesInMemory().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    long ComputeValueWithMask(long value, string mask)
    {
        long valueWithMask = 0;

        long power = 1L << 35;
        foreach (char maskBit in mask)
        {
            if (maskBit == '1' || (maskBit == 'X' && (value & power) != 0))
            {
                valueWithMask += power;
            }

            power >>= 1;
        }

        return valueWithMask;
    }

    long SumOfValuesInMemory()
    {
        return memory.Values.Sum();
    }
}
