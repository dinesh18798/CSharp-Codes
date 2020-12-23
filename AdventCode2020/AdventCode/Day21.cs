using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day21
    {
        readonly List<(string[] ingredients, string[] allergens)> foods;

        public Day21()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\foods.txt");

            foods = inputs.Select(input =>
            (ingredients: input.TrimEnd(')').Split("(contains")[0].Trim().Split(),
            allergens: input.TrimEnd(')').Split("(contains")[1].Trim().Replace(",", string.Empty).Split())).ToList();

            Operation(out int nonAllegrenIng, out string canonical);
            Console.WriteLine($"Part 1: {nonAllegrenIng}");
            Console.WriteLine($"Part 2: {canonical}");
        }

        void Operation(out int nonAllegrenIng, out string canonical)
        {
            Dictionary<string, List<string[]>> allergenToAllIngredients = foods.SelectMany(food => food.allergens).Distinct().
                ToDictionary(allergen => allergen, allergen => foods.Where(food => food.allergens.Contains(allergen)).
                Select(food => food.ingredients).ToList());

            SortedDictionary<string, string> allergenToIngredient = new SortedDictionary<string, string>();

            while (allergenToAllIngredients.Count > 0)
            {
                foreach (var allergen in allergenToAllIngredients.Keys)
                {
                    List<string[]> allIngredients = allergenToAllIngredients[allergen];
                    var possibleAllergenIngredients = allIngredients.Skip(1).Aggregate(new HashSet<string>(allIngredients.First()), (h, e) =>
                    {
                        h.IntersectWith(e);
                        return h;
                    });

                    if (possibleAllergenIngredients.Count == 1)
                    {
                        string possibleAllergenIngredient = possibleAllergenIngredients.ElementAt(0);
                        allergenToIngredient.Add(allergen, possibleAllergenIngredient);
                        allergenToAllIngredients.Remove(allergen);
                        allergenToAllIngredients = allergenToAllIngredients.ToDictionary(alg => alg.Key, alg => alg.Value.Select(ing => ing.
                          Where(i => i != possibleAllergenIngredient).ToArray()).ToList());
                        break;
                    }
                }
            }

            nonAllegrenIng = foods.Sum(food => food.ingredients.Count(ing => !allergenToIngredient.Values.Contains(ing)));
            canonical = string.Join(',', allergenToIngredient.Values);
        }
    }
}
