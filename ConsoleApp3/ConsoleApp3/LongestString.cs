using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class LongestString
    {
        internal string[] allLongestStrings(string[] inputArray)
        {
            List<string> result = new List<string>();
            int currLong = inputArray[0].Length;

            for (int i = 0; i < inputArray.Length; ++i)
            {
                string currentStr = inputArray[i];

                if (currentStr.Length == currLong)
                {
                    result.Add(currentStr);
                }
                else if (currentStr.Length > currLong)
                {
                    currLong = currentStr.Length;
                    result.Clear();
                    result.Add(currentStr);
                }
            }

            return result.ToArray();
        }

        internal string[] allLongestStringsLinq(string[] inputArray)
        {
            int longest = inputArray.Max(x => x.Length);
            return inputArray.Where(x => x.Length == longest).ToArray();
        }

        public int solution(int[] A)
        {
            List<int> sortList = A.OrderBy(x => x).ToList();
            int max = sortList.Max();

            if (max <= 0) return 1;

            int result = sortList[0] + 1;

            while (sortList.Contains(result))
            {
                ++result;
            }
            return result;
        }

    }
}
