using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventCode2020
{
    public class Day13
    {
        List<int> busId;
        int earliest;

        Dictionary<int, int> timeID;
        long wholeMultiplication = 1;

        public Day13()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\bus.txt");
            earliest = int.Parse(inputs[0]);
            busId = inputs[1].Replace(",x", string.Empty).Split(',').Select(int.Parse).ToList();

            timeID = new Dictionary<int, int>();
            string[] tempId = inputs[1].Split(',');
            for (int i = 0; i < tempId.Length; ++i)
            {
                if (int.TryParse(tempId[i], out int res))
                {
                    timeID.Add(res, res - i);
                    wholeMultiplication *= res;
                }
            }

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private int Part1()
        {
            bool isFound = false;
            int currTime = earliest;
            int currBusID = 0;
            while (true)
            {
                foreach (int bus in busId)
                {
                    if (currTime % bus == 0)
                    {
                        currBusID = bus;
                        isFound = true;
                        break;
                    }
                }

                if (isFound)
                    break;
                ++currTime;
            }

            return (currTime - earliest) * currBusID;
        }

        private long Part2()
        {
            long total = 0;
            foreach (var item in timeID)    
            {
                long temp = wholeMultiplication / item.Key;
                long bigInteger = (long)BigInteger.ModPow(temp, item.Key - 2, item.Key);
                total += (temp * bigInteger * item.Value);
            }
            return total % wholeMultiplication;
        }
    }
}
