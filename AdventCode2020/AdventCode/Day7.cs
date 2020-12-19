using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2020
{
    public class BaggageRule
    {
        public string Type;
        public string ContentString;
        public Dictionary<BaggageRule, int> Contents = new Dictionary<BaggageRule, int>();
        public List<BaggageRule> ContainedBy = new List<BaggageRule>();
    }

    public class Day7
    {
        string[] baggageRules;
        Dictionary<string, BaggageRule> ruleMap = new Dictionary<string, BaggageRule>();

        public Day7()
        {
            baggageRules = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode\baggages.txt");
            Regex ruleStart = new Regex("(.*?) bags contain (.*)");
            Regex ruleContents = new Regex("(\\d+|no) (.*?) bag");

            /* first we make each rule and add to the dictionary. then we will process each rules contents */
            foreach (string rule in baggageRules)
            {
                Match match = ruleStart.Match(rule);
                ruleMap.Add(match.Groups[1].Value, new BaggageRule() { Type = match.Groups[1].Value, ContentString = match.Groups[2].Value });
            }

            foreach (BaggageRule rule in ruleMap.Values)
            {
                MatchCollection matches = ruleContents.Matches(rule.ContentString);
                foreach (Match match in matches)
                {
                    string amount = match.Groups[1].Value;
                    string type = match.Groups[2].Value;
                    if (amount != "no")
                    {
                        BaggageRule containedType = ruleMap[type];
                        rule.Contents.Add(ruleMap[type], int.Parse(amount));
                        containedType.ContainedBy.Add(rule);
                    }
                }
            }
            Console.WriteLine($"Part 1 : {Part1()}");
            Console.WriteLine($"Part 2 : {Part2()}");
        }

        private int Part2()
        {
            BaggageRule shinyGoldRule = ruleMap["shiny gold"];

            int sum = 0;
            Stack<BaggageRule> stack = new Stack<BaggageRule>();
            stack.Push(shinyGoldRule);

            while (stack.Count > 0)
            {
                BaggageRule curRule = stack.Pop();
                sum += curRule.Contents.Values.Sum();

                foreach (BaggageRule key in curRule.Contents.Keys)
                {
                    for (int x = 0; x < curRule.Contents[key]; x++)
                    {
                        stack.Push(key);
                    }
                }
            }

            return sum;
        }

        private int Part1()
        {
            int sum = 0;
            Queue<string> bagsColours = new Queue<string>();
            HashSet<string> visitedBagColours = new HashSet<string>();
            bagsColours.Enqueue("shiny gold");

            while (bagsColours.Count != 0)
            {
                string currentBagColour = bagsColours.Dequeue();
                foreach (string line in baggageRules)
                {
                    if (line.Contains(currentBagColour))
                    {
                        int index = line.IndexOf(currentBagColour);
                        if (index > 0)
                        {
                            string newBagColour = line.Split(" bags")[0];
                            if (!bagsColours.Contains(newBagColour) && !visitedBagColours.Contains(newBagColour))
                            {
                                bagsColours.Enqueue(newBagColour);
                                ++sum;
                            }
                        }
                    }
                }
                visitedBagColours.Add(currentBagColour);
            }
            return sum;
        }
    }
}
