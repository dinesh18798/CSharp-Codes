using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] nums = { -1, 2, 1, -4 };
            Console.WriteLine($"Answer: {ThreeSumClosest(nums, 1)}");
        }

        static int LengthOfLongestSubstring(string s)
        {
            if (s.Length == 0) return 0;
            List<int> dict = new List<int>();
            dict.AddRange(Enumerable.Repeat(-1, 255));
            int maxLen = 0;
            int start = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (dict[s[i]] > start)
                {
                    start = dict[s[i]];
                }
                dict[s[i]] = i;
                maxLen = Math.Max(maxLen, i - start);
            }
            return maxLen;
        }

        static int Reverse(int x)
        {
            if (x >= 0 && x < 10)
                return x;

            bool isNegative = x < 0;

            long result = 0;
            x = isNegative ? -x : x;
            while (x != 0)
            {
                result = (result * 10) + (x % 10);
                x = x / 10;
            }
            if (isNegative)
                result = -(result);

            return (result < Int32.MinValue || result > Int32.MaxValue) ? 0 : (int)result;
        }

        static int ThreeSumClosest(int[] nums, int target)
        {

            int size = nums.Length;
            Array.Sort(nums);
            int result = nums[0] + nums[1] + nums[size - 1];

            for (int a = 0; a < size - 2; ++a)
            {
                int b = a + 1; int c = size - 1;

                while (b < c)
                {
                    int sum = nums[a] + nums[b] + nums[c];

                    if (sum == target) return sum;

                    if (Math.Abs(target - sum) < Math.Abs(target - result))
                        result = sum;

                    if (sum < target)
                        ++b;
                    else
                        --c;
                }
            }
            return result;
        }
    }
}
