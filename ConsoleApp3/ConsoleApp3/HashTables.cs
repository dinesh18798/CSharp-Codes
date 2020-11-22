using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp3
{
    public class HashTables
    {
        internal string[][] groupingDishes(string[][] dishes)
        {
            SortedDictionary<string, SortedSet<string>> ingredientDict = new SortedDictionary<string, SortedSet<string>>(StringComparer.Ordinal);
            int row = dishes.Length;

            foreach (string[] dish in dishes)
            {
                int column = dish.Length;
                for (int j = 1; j < column; j++)
                {
                    string ingName = dish[j];
                    if (ingredientDict.ContainsKey(ingName))
                        ingredientDict[ingName].Add(dish[0]);
                    else
                        ingredientDict.Add(ingName, new SortedSet<string>(StringComparer.Ordinal) { dish[0] });
                }
            }

            return ingredientDict.Where(x => x.Value.Count > 1).Select(i => new[] { i.Key }.Concat(i.Value).ToArray()).ToArray();
        }
    }

}
