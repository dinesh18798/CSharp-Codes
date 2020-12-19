using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day6
    {
        public Day6()
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }


        int Part1()
        {
            int sum = 0;

            string str = string.Empty;
            foreach (string myString in File.ReadAllLines(@"E:\custom.txt"))
            {
                if (string.IsNullOrEmpty(myString))
                {
                    HashSet<char> unique = new HashSet<char>(str);
                    sum += unique.Count;
                    str = string.Empty;
                }
                str += myString;
            }

            if (!string.IsNullOrEmpty(str))
            {
                HashSet<char> unique = new HashSet<char>(str);
                sum += unique.Count;
            }
            return sum;
        }

        int Part2()
        {
            int sum = 0;

            List<string> currentGroup = new List<string>();
            foreach (string myString in File.ReadAllLines(@"E:\custom.txt"))
            {
                if (string.IsNullOrEmpty(myString))
                {
                    sum += findEveryoneAnswered(currentGroup);
                    currentGroup = new List<string>();
                }
                else
                {
                    currentGroup.Add(String.Concat(myString.OrderBy(c => c)));
                }
            }

            if (currentGroup.Count > 0)
            {
                sum += findEveryoneAnswered(currentGroup);
            }
            return sum;
        }


        int findEveryoneAnswered(List<string> currentGroup)
        {
            List<char> commonAns = currentGroup[0].ToList();

            for (int i = 1; i < currentGroup.Count; i++)
            {
                List<char> tempCommon = new List<char>(commonAns);
                List<char> currAns = currentGroup[i].ToList();

                foreach (char chr in tempCommon)
                {
                    if (!currAns.Contains(chr))
                    {
                        commonAns.Remove(chr);
                    }
                }
            }

            return commonAns.Count;
        }

    }
}
