using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day14
    {
        string[] inputs;
        Dictionary<string, long> memoriesPart1;
        Dictionary<string, long> memoriesPart2;

        public Day14()
        {
            inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\bits.txt");
            memoriesPart1 = new Dictionary<string, long>();
            memoriesPart2 = new Dictionary<string, long>();

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private long Part1()
        {
            string currMask = string.Empty;

            foreach (string input in inputs)
            {
                if (input[1] == 'a')
                {
                    currMask = input.Split('=')[^1].Remove(0, 1);
                }
                else
                {
                    //mem[24196] = 465592
                    string value = input.Split('=')[^1].Remove(0, 1);
                    value = Convert.ToString(long.Parse(value), 2).PadLeft(currMask.Length, '0');

                    string addr = input.Split('=')[0].Remove(0, 4);
                    addr = addr.Remove(addr.Length - 2);

                    var maskAndValues = currMask.Zip(value, (x, y) => (Mask: x, Value: y));
                    string result = string.Concat(maskAndValues.Select(x => x.Mask != 'X' ? x.Mask : x.Value));
                    memoriesPart1[addr] = Convert.ToInt64(result, 2);
                }
            }

            return memoriesPart1.Keys.Sum(x => memoriesPart1[x]); ;
        }

        private long Part2()
        {
            string currMask = string.Empty;

            foreach (string input in inputs)
            {
                if (input[1] == 'a')
                {
                    currMask = input.Split('=')[^1].Remove(0, 1);
                }
                else
                {
                    //mem[24196] = 465592
                    long value = long.Parse(input.Split('=')[^1].Remove(0, 1));

                    string addr = input.Split('=')[0].Remove(0, 4);
                    addr = Convert.ToString(long.Parse(addr.Remove(addr.Length - 2)), 2).PadLeft(currMask.Length, '0');

                    var maskAndValues = currMask.Zip(addr, (x, y) => (Mask: x, Value: y));


                    string result = string.Concat(maskAndValues.Select(x => x.Mask != '0' ? x.Mask : x.Value));
                    List<string> combinations = GenerateCombination(result).Select(x => Convert.ToInt64(x, 2).ToString()).ToList();

                    foreach (string newAddr in combinations)
                    {
                        memoriesPart2[newAddr] = value;
                    }
                }
            }

            return memoriesPart2.Keys.Sum(x => memoriesPart2[x]); ;
        }

        private HashSet<string> GenerateCombination(string addr)
        {
            if (!addr.Contains('X'))
            {
                return new HashSet<string>() { addr };
            }
            else
            {
                string zeroMask = ReplaceFirstMatch(addr, "X", "0");
                string oneMask = ReplaceFirstMatch(addr, "X", "1");

                return GenerateCombination(zeroMask).Concat(GenerateCombination(oneMask)).ToHashSet();
            }
        }

        private string ReplaceFirstMatch(string addr, string old, string replace)
        {
            int last = addr.LastIndexOf(old);

            if (last >= 0)
            {
                addr = addr.Remove(last,1).Insert(last, replace);
            }
            return addr;
        }
    }
}
