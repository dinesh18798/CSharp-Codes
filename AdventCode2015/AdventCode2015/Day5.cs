using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    public class Day5
    {
        string[] texts;
        public Day5()
        {
            texts = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\texts.txt");
            List<string> part1Texts = new List<string>();

            Console.WriteLine($"Part 1: {Part1(ref part1Texts)}");
            Console.WriteLine($"Part 2: {Part2(part1Texts)}");
        }

        private int Part2(List<string> part1Texts)
        {
            int niceCount = 0;

            Regex anyTwoRepeat = new Regex("^.*?([a-z]{2}).*?(\\1).*$");
            Regex oneLetterBetween = new Regex("([a-z]).\\1");

            foreach (string text in texts)
            {
                if (anyTwoRepeat.IsMatch(text) && oneLetterBetween.IsMatch(text))
                {
                    ++niceCount;
                }
            }
            return niceCount;
        }

        private int Part1(ref List<string> correctText)
        {
            int niceCount = 0;

            Regex repeat = new Regex("([a-z])\\1{1,}");
            Regex vowels = new Regex("([aeiou].*[aeiou].*[aeiou])");
            Regex exclude = new Regex("^(?!.*ab)(?!.*cd)(?!.*pq)(?!.*xy).*$");

            foreach (string text in texts)
            {
                if (exclude.IsMatch(text))
                {
                    if (repeat.IsMatch(text) && vowels.IsMatch(text))
                    {
                        correctText.Add(text);
                        ++niceCount;
                    }
                }
            }

            return niceCount;
        }
    }
}
