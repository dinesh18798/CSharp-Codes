using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2015
{
    public class Day1
    {
        public Day1()
        {
            string floor = File.ReadAllText(@"E:\GitHub\CSharp-Codes\AdventCode2015\floor.txt");
            Console.WriteLine($"Part 1: {Part1(floor)}");
            Console.WriteLine($"Part 2: {Part2(floor)}");
        }

        private int Part2(string floor)
        {
            int currFloor = 0;
            int index = 0;
            for (; index < floor.Length; ++index)
            {
                if (floor[index] == ')')
                {
                    --currFloor;
                }
                else if (floor[index] == '(')
                {
                    ++currFloor;
                }

                if (currFloor == -1)
                {
                    break;
                }
            }

            return index + 1;
        }

        private int Part1(string fl)
        {
            string floor = new string(fl);
            int currFloor = 0;
            while (floor.Contains("()"))
            {
                floor = floor.Replace("()", string.Empty);
            }

            while (floor.Contains(")("))
            {
                floor = floor.Replace(")(", string.Empty);
            }

            foreach (char c in floor)
            {
                if (c == ')')
                {
                    --currFloor;
                }
                else if (c == '(')
                {
                    ++currFloor;
                }
            }

            return currFloor;
        }
    }
}
