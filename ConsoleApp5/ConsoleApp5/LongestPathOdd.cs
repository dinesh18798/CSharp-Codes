using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    public class LongestPathOdd
    {
        List<List<int>> citiesNetwork;

        public int longestPath(int[] T)
        {
            citiesNetwork = new List<List<int>>();
            int n = T.Length;
            for (int i = 0; i < n; i++)
            {
                citiesNetwork.Add(new List<int>());
            }
            //we do this to denote that there is an edge between u and T[u] and vice-versa
            for (int u = 0; u < n; u++)
            {
                if (u != T[u])
                {
                    citiesNetwork[u].Add(T[u]);
                    citiesNetwork[T[u]].Add(u);
                }
            }

            return recurse(0, -1, false);
        }

        int recurse(int node, int parent, bool usedTicket)
        {
            //if we enter an odd numbered node and we have already 
            //visited another odd-valued one, then we cannot proceed and just return 0
            if (usedTicket && node % 2 == 1) return 0;

            //find out how deep can we go, given the restriction
            //we can visit at most one odd-valued node
            int max = 0;
            foreach (int next in citiesNetwork[node])
            {
                if (next != parent)
                {
                    max = Math.Max(max, recurse(next, node, usedTicket | (node % 2 == 1)));
                }
            }

            //we add 1 to the answer, because we should 
            //also count the node we are currently at
            return 1 + max;
        }
    }
}
