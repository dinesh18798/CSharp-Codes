using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCode2020
{
    public struct Rule
    {
        public int rule1;
        public int rule2;
    }

    public class Rules
    {
        public bool isChecked = false;
        List<Rule> rules;
        List<string> pattern;
        public Rules()
        {
            rules = new List<Rule>();
            pattern = new List<string>();
        }
    }

    public class Day19
    {
        Dictionary<int, string> overallRules;
        HashSet<string> messages;
        public Day19()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\rules.txt");

            overallRules = new Dictionary<int, string>();
            int i = 0;

            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;

                string line = inputs[i];
                string rul = line.Split(':')[1][1..];

                if (rul.StartsWith("\""))
                    rul = rul.Substring(1, rul.Length - 2);
                overallRules[int.Parse(line.Split(':')[0])] = rul;
            }

            ++i;
            messages = new HashSet<string>();
            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;
                string line = inputs[i];
                messages.Add(inputs[i]);
            }

            Console.WriteLine($"Part 1: {Validation(false)}");
            Console.WriteLine($"Part 2: {Validation(true)}");
        }

        private int Validation(bool part2)
        {
            Dictionary<int, string> tempRules = new Dictionary<int, string>(overallRules);
            int validMessages = 0;
            string rule = tempRules[0];

            if (part2)
            {
                tempRules[8] = "42 | 42 (42 | 42 (42 | 42 (42 | 42 (42 | 42))))";
                tempRules[11] = "42 31 | 42 (42 31 | 42 (42 31 | 42 (42 31 | 42 31) 31) 31) 31";
            }

            Regex regex = new Regex(@"\d+");

            while (regex.IsMatch(rule))
            {
                Match match = regex.Match(rule);
                string appendRule = "(" + tempRules[int.Parse(match.Value)] + ")";
                rule = regex.Replace(rule, appendRule, 1);
            }

            rule = '^' + rule.Replace(" ", string.Empty) + '$';
            Regex ruleRegex = new Regex(@rule);

            foreach (string message in messages)
            {
                if (ruleRegex.IsMatch(message))
                {
                    ++validMessages;
                }
            }

            return validMessages;
        }
    }
}
