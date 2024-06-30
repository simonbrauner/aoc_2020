namespace AdventOfCode.Solutions.Year2020.Day03;

class TreeMap
{
    String[] trees;

    public TreeMap(String[] trees)
    {
        this.trees = trees;
    }

    public bool TreeOnCoordinates(int x, int y)
    {
        return trees[y][x % trees[0].Length] == '#';
    }

    public int RowCount()
    {
        return trees.Length;
    }
}

class Solution : SolutionBase
{
    TreeMap treeMap;

    public Solution()
        : base(03, 2020, "")
    {
        treeMap = new TreeMap(Input.SplitByNewline());
    }

    protected override string SolvePartOne()
    {
        int x = 0;
        int y = 0;
        int result = 0;

        while (++y < treeMap.RowCount())
        {
            x += 3;

            if (treeMap.TreeOnCoordinates(x, y))
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
