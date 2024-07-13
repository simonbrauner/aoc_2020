namespace AdventOfCode.Solutions.Year2020.Day08;

using System.Text.RegularExpressions;

record Instruction(string Operation, int Argument);

class Solution : SolutionBase
{
    List<Instruction> instructions = new List<Instruction>();

    public Solution()
        : base(08, 2020, "")
    {
        foreach (string line in Input.SplitByNewline())
        {
            Match instruction = Regex.Match(line, @"(acc|jmp|nop) ((?:\+|-)\d+)");

            instructions.Add(
                new(instruction.Groups[1].Value, int.Parse(instruction.Groups[2].Value))
            );
        }
    }

    protected override string SolvePartOne()
    {
        int instructionIndex = 0;
        HashSet<int> usedInstructionIndices = new HashSet<int>();
        int accumulator = 0;

        while (!usedInstructionIndices.Contains(instructionIndex))
        {
            Instruction instruction = instructions[instructionIndex];
            usedInstructionIndices.Add(instructionIndex);

            switch (instruction.Operation)
            {
                case "acc":
                    accumulator += instruction.Argument;
                    instructionIndex++;
                    break;
                case "jmp":
                    instructionIndex += instruction.Argument;
                    break;
                case "nop":
                    instructionIndex++;
                    break;
            }
        }

        return accumulator.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
