using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    public class CountersClass
    {

        public int[] Solution(int N, int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int[] counters = new int[N];
            int overallMax = 0;
            int currentMax = 0;

            for (int i = 0; i < A.Length; ++i)
            {
                if (A[i] > N)
                    overallMax = currentMax;
                else
                {
                    int pos = A[i] - 1;
                    if (counters[pos] < overallMax)
                        counters[pos] = overallMax + 1;
                    else
                        counters[pos]++;
                    currentMax = Math.Max(currentMax, counters[pos]);
                }
            }

            //counters = counters.Select(x => x < overallMax ? overallMax : x).ToArray();

            for (int j = 0; j < N; ++j)
            {
                if (counters[j] < overallMax)
                    counters[j] = overallMax;
            }

            return counters;
        }

    }
}
