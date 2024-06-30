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
        return CountTrees(3, 1).ToString();
    }

    protected override string SolvePartTwo()
    {
        (int, int)[] movements = { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

        long product = 1;

        foreach ((int right, int down) in movements)
        {
            product *= CountTrees(right, down);
        }

        return product.ToString();
    }

    int CountTrees(int right, int down)
    {
        int x = 0;
        int y = 0;
        int result = 0;

        while ((y += down) < treeMap.RowCount())
        {
            x += right;

            if (treeMap.TreeOnCoordinates(x, y))
            {
                result++;
            }
        }

        return result;
    }
}
