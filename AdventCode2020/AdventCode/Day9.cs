using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day9
    {
        readonly List<long> portoutputs;

        public Day9()
        {
            portoutputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\portoutputs.txt").Select(long.Parse).ToList();

            long part1 = Part1();
            Console.WriteLine($"Part 1 : {part1}");
            Console.WriteLine($"Part 2 : {Part2(part1)}");
        }

        private long Part2(long part1)
        {
            long result = 0;
            List<long> numList = new List<long>();
            for (int i = 0; i < portoutputs.Count - 1; ++i)
            {
                long tempSum = portoutputs[i];
                numList.Add(portoutputs[i]);
                for (int j = i + 1; j < portoutputs.Count; ++j)
                {
                    numList.Add(portoutputs[j]);
                    tempSum += portoutputs[j];
                    if (tempSum >= part1)
                    {
                        break;
                    }
                }
                if (tempSum == part1)
                {
                    break;
                }
                numList.Clear();
            }

            if (numList.Count > 1)
            {
                result = numList.Max() + numList.Min();
            }
            return result;
        }

        private long Part1()
        {
            int preamble = 25;
            long result = 0;
            for (int i = 0; i + preamble < portoutputs.Count; ++i)
            {
                result = portoutputs[i + preamble];

                List<long> tempList = portoutputs.GetRange(i, preamble);

                if (!CheckSum(result, tempList))
                {
                    break;
                }
            }

            return result;
        }

        private bool CheckSum(long findNumber, List<long> numbers)
        {
            bool res = false;
            numbers = numbers.OrderBy(x => x).ToList();
            int front = 0;
            int back = numbers.Count - 1;

            while (front < back)
            {
                long tempSum = numbers[front] + numbers[back];

                if (tempSum == findNumber)
                {
                    res = true;
                    break;
                }
                else if (tempSum < findNumber)
                {
                    ++front;
                }
                else
                {
                    --back;
                }
            }

            return res;
        }
    }
}
