using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdventCode2015
{
    public class Day4
    {
        public Day4()
        {
            Part1And2(out int part1, out int part2);
            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        private void Part1And2(out int part1, out int part2)
        {
            part1 = 0;
            part2 = 0;
            string input = "bgvyzdsv";
            int counter = 1;

            HashAlgorithm hashAlgorithm = MD5.Create();
            while (true)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + counter.ToString());
                byte[] hashValue = hashAlgorithm.ComputeHash(bytes);
                string result = BitConverter.ToString(hashValue).Replace("-", string.Empty).ToLower();

                if (result.StartsWith("00000") && part1 == 0)
                {
                    part1 = counter;
                }
                if (result.StartsWith("000000"))
                {
                    part2 = counter;
                    break;
                }
                ++counter;
            }
        }
    }
}
