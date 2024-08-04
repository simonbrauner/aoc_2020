namespace AdventOfCode.Solutions.Year2020.Day21;

using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    List<HashSet<string>> ingredients = new List<HashSet<string>>();
    Dictionary<string, List<int>> allergens = new Dictionary<string, List<int>>();

    public Solution()
        : base(21, 2020, "")
    {
        int index = 0;
        foreach (string line in Input.SplitByNewline())
        {
            string[] parts = line.Split(" (");

            ingredients.Add(new HashSet<string>(parts[0].Split(' ')));

            foreach (Match match in Regex.Matches(parts[1], @"\w+").Skip(1))
            {
                string allergenName = match.Value;
                if (!allergens.ContainsKey(allergenName))
                {
                    allergens.Add(allergenName, new List<int>() { index });
                }
                else
                {
                    allergens[allergenName].Add(index);
                }
            }

            index++;
        }
    }

    protected override string SolvePartOne()
    {
        HashSet<string> foundAllergens = new HashSet<string>();
        HashSet<string> ingredientsWithAllergens = new HashSet<string>();

        while (foundAllergens.Count() != allergens.Count())
        {
            foreach (string allergenName in allergens.Keys.Except(foundAllergens))
            {
                List<int> indices = allergens[allergenName];

                HashSet<string> possibleIngredients = new HashSet<string>(ingredients[indices[0]]);
                foreach (int index in indices.Skip(1))
                {
                    possibleIngredients.IntersectWith(ingredients[index]);
                }

                if (possibleIngredients.Count() == 1)
                {
                    foundAllergens.Add(allergenName);

                    string ingredientName = possibleIngredients.Single();
                    ingredientsWithAllergens.Add(ingredientName);
                    foreach (int removeIndex in indices)
                    {
                        ingredients[removeIndex].Remove(ingredientName);
                    }
                }
            }
        }

        return ingredients.Select(i => i.Except(ingredientsWithAllergens).Count()).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
