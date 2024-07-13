namespace AdventOfCode.Solutions.Year2020.Day08;

using System.Text.RegularExpressions;

record Instruction(string Operation, int Argument);

record ProgramResult(int AccumulatorValue, bool Stopped);

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
        return RunProgram().AccumulatorValue.ToString();
    }

    protected override string SolvePartTwo()
    {
        for (int i = 0; i < instructions.Count(); i++)
        {
            Instruction originalInstruction = instructions[i];

            if (originalInstruction.Operation == "jmp")
            {
                instructions[i] = new("nop", originalInstruction.Argument);
            }
            else if (originalInstruction.Operation == "nop")
            {
                instructions[i] = new("jmp", originalInstruction.Argument);
            }

            ProgramResult programResult = RunProgram();
            if (programResult.Stopped)
            {
                return programResult.AccumulatorValue.ToString();
            }

            instructions[i] = originalInstruction;
        }

        return "";
    }

    ProgramResult RunProgram()
    {
        int instructionIndex = 0;
        HashSet<int> usedInstructionIndices = new HashSet<int>();
        int accumulator = 0;

        while (true)
        {
            if (usedInstructionIndices.Contains(instructionIndex))
            {
                return new(accumulator, false);
            }
            if (instructionIndex == instructions.Count())
            {
                return new(accumulator, true);
            }

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
    }
}
