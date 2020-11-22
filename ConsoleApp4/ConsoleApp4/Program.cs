using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            //alternatingSums(new int[] { 50, 60, 60, 45, 70 });
            //addBorder(new string[] { "abc", "ded" });

            //Console.WriteLine($"Result : { areSimilar(new int[] { 4, 6, 3 }, new int[] { 3, 4, 6 })}");

            //arrayChange(new int[] { 1,1,1 });

            //palindromeRearranging("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaabc");

            //possibleSums(new int[] { 10, 50, 100 }, new int[] { 1, 2, 1 });

            /*int[][] pairs = new int[][]
            {
                new int[]{1,3},
                new int[]{6,8},
                new int[]{3,8},
                new int[]{2,7}
            };
            swapLexOrder("acxrabdz", pairs);*/

            isIPv4Address("1.1.1.1a");

            avoidObstacles(new int[] { 2, 3 });
        }

        static int[] alternatingSums(int[] a)
        {
            int team1 = 0;
            int team2 = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (i % 2 == 0)
                    team1 += a[i];
                else
                    team2 += a[i];
            }

            return new int[] { team1, team2 };

        }

        static string[] addBorder(string[] picture)
        {
            List<string> border = picture.Select(s => '*' + s + '*').ToList();
            border.Insert(0, new string('*', border[0].Length));
            border.Add(new string('*', border[0].Length));

            return border.ToArray();
        }

        static bool areSimilar(int[] a, int[] b)
        {
            int swapCount = 0;
            List<int> aList = new List<int>();
            List<int> bList = new List<int>();

            for (int i = 0; i < a.Length; i++)
            {
                aList.Add(a[i]);
                bList.Add(b[i]);

                if (a[i] != b[i])
                    swapCount++;

                if (swapCount > 2)
                    return false;

            }

            return aList.OrderBy(x => x).SequenceEqual(bList.OrderBy(x => x));
        }

        static int arrayChange(int[] inputArray)
        {

            int minimal = 0;
            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                if (inputArray[i] >= inputArray[i + 1])
                {
                    int exist = inputArray[i + 1];
                    inputArray[i + 1] = inputArray[i] + 1;
                    minimal += inputArray[i + 1] - exist;
                }
            }

            return minimal;

        }

        static bool palindromeRearranging(string inputString)
        {
            bool oddExist = false;
            HashSet<char> characters = new HashSet<char>(inputString);

            foreach (char c in characters)
            {
                int count = inputString.Count(x => x == c);

                if (count % 2 == 0)
                    continue;
                if (inputString.Length % 2 == 0 || oddExist)
                    return false;
                oddExist = true;
            }
            return true;
        }

        bool areFollowingPatterns(string[] strings, string[] patterns)
        {
            Dictionary<string, string> matchDict = new Dictionary<string, string>();

            for (int i = 0; i < strings.Length; i++)
            {
                if (matchDict.ContainsKey(strings[i]))
                {
                    if (matchDict[strings[i]] != patterns[i])
                        return false;
                }
                if (matchDict.ContainsValue(patterns[i]))
                {
                    if (strings[i] != matchDict.FirstOrDefault(x => x.Value == patterns[i]).Key)
                        return false;
                }
                else
                {
                    matchDict.Add(strings[i], patterns[i]);
                }

            }
            return true;
        }


        bool containsCloseNums(int[] nums, int k)
        {
            Dictionary<int, int> existedNums = new Dictionary<int, int>();
            bool result = false;

            for (int i = 0; i < nums.Length; i++)
            {
                if (existedNums.ContainsKey(nums[i]))
                {
                    if (i - existedNums[nums[i]] <= k)
                        return true;
                    existedNums[nums[i]] = i;
                }
                else
                    existedNums.Add(nums[i], i);
            }

            return result;
        }

        static int possibleSums(int[] coins, int[] quantity)
        {
            HashSet<int> set = new HashSet<int>();
            // extra entry to guarantee current can be combined with prev, starting at 0
            set.Add(0);

            for (int i = 0; i < coins.Length; i++)
            {
                HashSet<int> next = new HashSet<int>();
                foreach (int prev in set)
                {
                    for (int j = 1; j <= quantity[i]; j++)
                    {
                        next.Add(prev + j * coins[i]);
                    }
                }
                // combine all new entries with global before next coin
                set.UnionWith(next);
            }

            // subract 1 for 0 added in line 3
            return set.Count - 1;

        }

        static string swapLexOrder(string str, int[][] pairs)
        {
            string largeLex = str;
            while (true)
            {
                char[] characters = largeLex.ToCharArray();

                for (int i = 0; i < pairs.Length; i++)
                {
                    int firstPos = pairs[i][0] - 1;
                    int secondPos = pairs[i][1] - 1;

                    char temp = characters[firstPos];
                    characters[firstPos] = characters[secondPos];
                    characters[secondPos] = temp;
                }

                string newString = new string(characters);

                if (String.Compare(newString, largeLex) < 0)
                    break;
                largeLex = newString;
            }

            return largeLex;
        }

        static bool isIPv4Address(string inputString)
        {
            string[] ipAddress = inputString.Split('.');

            if (ipAddress.Length != 4) return false;

            foreach (string addr in ipAddress)
            {
                if (string.IsNullOrEmpty(addr) || (addr.Length > 1 && addr[0] == '0'))
                    return false;

                bool success = Int32.TryParse(addr, out int addrNum);

                if (!success) return false;

                if (addrNum < 0 || addrNum > 255)
                    return false;
            }
            return true;
        }

        static int avoidObstacles(int[] inputArray)
        {

            int[] sortedArray = inputArray.OrderBy(x => x).ToArray();
            int maxObstacle = sortedArray.Max();

            int jumpLength = 0;
            while (true)
            {
                jumpLength++;
                int position = jumpLength;

                while(!inputArray.Contains(position))
                {
                    position += jumpLength;

                    if (position > maxObstacle)
                        return jumpLength;
                }
            }
        }


    }
}
