using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day10
    {
        List<int> jolts;
        public Day10()
        {
            jolts = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\jolts.txt").Select(int.Parse).OrderBy(x => x).ToList();
            jolts.Insert(0, 0);
            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private int Part1()
        {
            int onesCount = 0;
            int threesCount = 1;

            int currJolt = jolts[0];

            for (int i = 1; i < jolts.Count; ++i)
            {
                if (jolts[i] - currJolt == 1)
                {
                    ++onesCount;
                }
                else if (jolts[i] - currJolt == 3)
                {
                    ++threesCount;
                }
                currJolt = jolts[i];
            }
            return onesCount * threesCount;
        }

        private long Part2()
        {
            long[] match = new long[jolts.Count];

            match[^1] = 1;

            for (int i = jolts.Count - 2; i >= 0; --i)
            {
                for (int j = i + 1; j < jolts.Count; j++)
                {
                    if (jolts[j] - jolts[i] <= 3)
                        match[i] += match[j];
                    else
                        break;
                }
            }
            return match[0];
        }
    }
}
