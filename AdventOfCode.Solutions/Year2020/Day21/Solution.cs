namespace AdventOfCode.Solutions.Year2020.Day21;

using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    List<HashSet<string>> ingredients = new List<HashSet<string>>();
    Dictionary<string, List<int>> allergens = new Dictionary<string, List<int>>();
    Dictionary<string, string> allergenIngredients = new Dictionary<string, string>();

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
        DetermineAllergenIngredients();

        return CountIngredientsWithoutAllergens().ToString();
    }

    protected override string SolvePartTwo()
    {
        List<string> allergenNames = new List<string>(allergenIngredients.Keys);
        allergenNames.Sort();

        return String.Join(',', allergenNames.Select(n => allergenIngredients[n]));
    }

    void DetermineAllergenIngredients()
    {
        while (allergenIngredients.Count() != allergens.Count())
        {
            foreach (string allergenName in allergens.Keys.Except(allergenIngredients.Keys))
            {
                List<int> indices = allergens[allergenName];

                HashSet<string> possibleIngredients = new HashSet<string>(ingredients[indices[0]]);
                foreach (int index in indices.Skip(1))
                {
                    possibleIngredients.IntersectWith(ingredients[index]);
                }

                if (possibleIngredients.Count() == 1)
                {
                    string ingredientName = possibleIngredients.Single();
                    allergenIngredients.Add(allergenName, ingredientName);

                    foreach (int removeIndex in indices)
                    {
                        ingredients[removeIndex].Remove(ingredientName);
                    }
                }
            }
        }
    }

    int CountIngredientsWithoutAllergens()
    {
        HashSet<string> ingredientsWithAllergens = allergenIngredients.Values.ToHashSet();

        return ingredients.Select(i => i.Except(ingredientsWithAllergens).Count()).Sum();
    }
}
