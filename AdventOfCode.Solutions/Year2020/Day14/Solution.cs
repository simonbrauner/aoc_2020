namespace AdventOfCode.Solutions.Year2020.Day14;

using System.Text.RegularExpressions;

record Instruction();

record UpdateBitmask(string Mask) : Instruction;

record WriteValue(long Address, long Value) : Instruction;

abstract class DecoderChip
{
    protected string mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

    public abstract Dictionary<long, long> Memory(List<Instruction> instructions);
}

class DecoderChipVersionOne : DecoderChip
{
    public override Dictionary<long, long> Memory(List<Instruction> instructions)
    {
        Dictionary<long, long> memory = new Dictionary<long, long>();

        foreach (Instruction instruction in instructions)
        {
            switch (instruction)
            {
                case UpdateBitmask updateBitmask:
                    mask = updateBitmask.Mask;
                    break;
                case WriteValue writeValue:
                    memory[writeValue.Address] = ComputeValueWithMask(writeValue.Value);
                    break;
            }
        }

        return memory;
    }

    long ComputeValueWithMask(long value)
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
}

class DecoderChipVersionTwo : DecoderChip
{
    public override Dictionary<long, long> Memory(List<Instruction> instructions)
    {
        Dictionary<long, long> memory = new Dictionary<long, long>();

        foreach (Instruction instruction in instructions)
        {
            switch (instruction)
            {
                case UpdateBitmask updateBitmask:
                    mask = updateBitmask.Mask;
                    break;
                case WriteValue writeValue:
                    foreach (long address in ComputeAddressesWithMask(writeValue.Address))
                    {
                        memory[address] = writeValue.Value;
                    }
                    break;
            }
        }

        return memory;
    }

    List<long> ComputeAddressesWithMask(long address)
    {
        long addressWithoutFloatingBits = 0;
        Stack<long> floatingBits = new Stack<long>();

        long power = 1L << 35;
        foreach (char maskBit in mask)
        {
            if (maskBit == '1' || (maskBit == '0' && (address & power) != 0))
            {
                addressWithoutFloatingBits += power;
            }

            if (maskBit == 'X')
            {
                floatingBits.Push(power);
            }

            power >>= 1;
        }

        List<long> addresses = new List<long>();
        ExpandFloatingBits(addressWithoutFloatingBits, floatingBits, addresses);

        return addresses;
    }

    void ExpandFloatingBits(long address, Stack<long> floatingBits, List<long> addresses)
    {
        if (floatingBits.Count() == 0)
        {
            addresses.Add(address);
            return;
        }

        long currentBit = floatingBits.Pop();

        ExpandFloatingBits(address, floatingBits, addresses);
        ExpandFloatingBits(address + currentBit, floatingBits, addresses);

        floatingBits.Push(currentBit);
    }
}

class Solution : SolutionBase
{
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
        return (new DecoderChipVersionOne().Memory(instructions)).Values.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return (new DecoderChipVersionTwo().Memory(instructions)).Values.Sum().ToString();
    }
}
