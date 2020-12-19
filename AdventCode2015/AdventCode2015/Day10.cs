using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    public class Day10
    {
        public Day10()
        {
            string input = "3113322113";
            Part(input, 50, out int part1, out int part2);
            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private void Part(string input, int numOfTurns, out int part1, out int part2)
        {
            string currInput = input;
            string inputAt40 = string.Empty;
            for (int i = 0; i < numOfTurns; ++i)
            {
                string newInput = string.Empty;
                MatchCollection match = Regex.Matches(currInput, "(\\d)\\1*");

                newInput = string.Concat(match.Select(m => m.Value.Length.ToString() + m.Value[0].ToString()));
                currInput = newInput;

                if (i == 39)
                    inputAt40 = currInput;
            }
            part1 = inputAt40.Length;
            part2 = currInput.Length;
        }
    }
}
