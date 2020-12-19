using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    class Day8
    {
        List<string> texts;
        public Day8()
        {
            texts = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\digital.txt").ToList();
            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private int Part1()
        {
            int diff = 0;
            texts.ForEach(t =>
            {
                diff += t.Length - Regex.Replace(t.Trim('"').Replace("\\\"", "Q").Replace("\\\\", "W"), "\\\\x\\w{2}", "H").Length;
            });
            return diff;
        }

        private int Part2()
        {
            int diff = 0;
            texts.ForEach(t =>
            {
                diff += t.Replace("\\", "QQ").Replace("\"", "WW").Length + 2 - t.Length;
            });
            return diff;
        }
    }
}
