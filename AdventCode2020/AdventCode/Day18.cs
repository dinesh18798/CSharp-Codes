using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCode2020
{
    public class Day18
    {
        string[] inputs;
        public Day18()
        {
            inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\expression.txt");
            Evaluation(out long part1, out long part2);
            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private void Evaluation(out long part1, out long part2)
        {
            part1 = 0;
            part2 = 0;
            Regex expression = new Regex(@"\(([^()]+)\)");
            foreach (string input in inputs)
            {
                string currInputPart1 = input;
                while (expression.IsMatch(currInputPart1))
                {
                    currInputPart1 = expression.Replace(currInputPart1, m => EvaluationPart1(m.Groups[1].Value));
                }
                part1 += long.Parse(EvaluationPart1(currInputPart1));

                string currInputPart2 = input;
                while (expression.IsMatch(currInputPart2))
                {
                    currInputPart2 = expression.Replace(currInputPart2, m => EvaluationPart2(m.Groups[1].Value));
                }
                part2 += long.Parse(EvaluationPart2(currInputPart2));
            }
        }

        private string EvaluationPart1(string expression)
        {
            Regex evaluation = new Regex(@"(\d+) (\+|\*) (\d+)");

            while (evaluation.IsMatch(expression))
            {
                expression = evaluation.Replace(expression, m => m.Groups[2].Value == "*"
                ? (long.Parse(m.Groups[1].Value) * long.Parse(m.Groups[3].Value)).ToString()
                : (long.Parse(m.Groups[1].Value) + long.Parse(m.Groups[3].Value)).ToString(), 1);
            }
            return expression;
        }

        private string EvaluationPart2(string expression)
        {
            Regex additionEval = new Regex(@"(\d+) (\+) (\d+)");
            while (additionEval.IsMatch(expression))
            {
                expression = additionEval.Replace(expression, m =>
                (long.Parse(m.Groups[1].Value) + long.Parse(m.Groups[3].Value)).ToString(), 1);
            }

            Regex multiplicationEval = new Regex(@"(\d+) (\*) (\d+)");
            while (multiplicationEval.IsMatch(expression))
            {
                expression = multiplicationEval.Replace(expression, m =>
                (long.Parse(m.Groups[1].Value) * long.Parse(m.Groups[3].Value)).ToString(), 1);
            }
            return expression;
        }
    }
}
