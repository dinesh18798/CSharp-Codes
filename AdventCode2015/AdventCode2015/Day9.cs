using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    public class Day9
    {
        Dictionary<Tuple<string, string>, int> locations;
        private List<string> allTowns;

        public Day9()
        {
            Dictionary<string, int> cityID = new Dictionary<string, int>();

            locations = new Dictionary<Tuple<string, string>, int>();
            allTowns = new List<string>();

            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\routes.txt");
            int longestName = -1;

            int id = 0;
            foreach (string input in inputs)
            {
                GroupCollection groups = Regex.Match(input, "(\\w+) to (\\w+) = (\\d+)").Groups;

                if (!cityID.ContainsKey(groups[1].Value))
                {
                    cityID.Add(groups[1].Value, id++);
                    longestName = Math.Max(groups[1].Value.Length, longestName);
                }
                if (!cityID.ContainsKey(groups[2].Value))
                {
                    cityID.Add(groups[2].Value, id++);
                    longestName = Math.Max(groups[2].Value.Length, longestName);
                }
            }

            int[,] cityMap = new int[cityID.Count, cityID.Count];

            foreach (string input in inputs)
            {
                GroupCollection groups = Regex.Match(input, "(\\w+) to (\\w+) = (\\d+)").Groups;
                cityMap[cityID[groups[1].Value], cityID[groups[2].Value]] = int.Parse(groups[3].Value);
                cityMap[cityID[groups[2].Value], cityID[groups[1].Value]] = int.Parse(groups[3].Value);

                locations[new Tuple<string, string>(groups[1].Value, groups[2].Value)] = int.Parse(groups[3].Value);
                locations[new Tuple<string, string>(groups[2].Value, groups[1].Value)] = int.Parse(groups[3].Value);

                if (!allTowns.Contains(groups[1].Value))
                    allTowns.Add(groups[1].Value);

                if (!allTowns.Contains(groups[2].Value))
                    allTowns.Add(groups[2].Value);
            }

            ProcessPermutations();
        }

        public static List<List<string>> BuildPermutations(List<string> items)
        {
            if (items.Count > 1)
            {
                return items.SelectMany(item => BuildPermutations(items.Where(i => !i.Equals(item)).ToList()),
                                       (item, permutation) => new[] { item }.Concat(permutation).ToList()).ToList();
            }

            return new List<List<string>> { items };
        }

        private void ProcessPermutations()
        {
            long minTripLength = long.MaxValue;
            long maxTripLength = 0;

            List<List<string>> allPermutations = BuildPermutations(allTowns);
            foreach (List<string> thisPermutation in allPermutations)
            {
                long tripLength = 0;
                for (int i = 0; i < thisPermutation.Count - 1; i++)
                    tripLength += locations[new Tuple<string, string>(thisPermutation[i], thisPermutation[i + 1])];

                minTripLength = Math.Min(tripLength, minTripLength);
                maxTripLength = Math.Max(tripLength, maxTripLength);
            }

            Console.WriteLine("Min: {0}", minTripLength);
            Console.WriteLine("Max: {0}", maxTripLength);
        }
    }
    //Travel Salesman Algorithm
    /*
    internal class TspDynamicProgrammingRecursive
    {

        private int N;
        private int START_NODE;
        private int FINISHED_STATE;

        private int[,] distance;
        private int minTourCost = int.MaxValue;

        private List<int> tour = new List<int>();
        private bool ranSolver = false;

        public TspDynamicProgrammingRecursive(int[,] distance) : this(0, distance) { }

        public TspDynamicProgrammingRecursive(int startNode, int[,] distance)
        {

            this.distance = distance;
            N = distance.GetLength(0);
            START_NODE = startNode;

            FINISHED_STATE = (1 << N) - 1;
        }

        // Returns the optimal tour for the traveling salesman problem.
        public List<int> getTour()
        {
            if (!ranSolver) solve();
            return tour;
        }

        // Returns the minimal tour cost.
        public int getTourCost()
        {
            if (!ranSolver)
                solve();
            return minTourCost - distance[tour[^2], 0];
        }

        public void solve()
        {

            // Run the solver
            int state = 1 << START_NODE;
            int c = 1 << N;
            int[,] memo = new int[N, c];
            int[,] prev = new int[N, c];
            minTourCost = TravelSalesman(START_NODE, state, memo, prev);

            // Regenerate path
            int index = START_NODE;
            while (true)
            {
                tour.Add(index);
                int nextIndex = prev[index, state];
                if (nextIndex == 0) break;
                int nextState = state | (1 << nextIndex);
                state = nextState;
                index = nextIndex;
            }
            tour.Add(START_NODE);
            ranSolver = true;
        }

        private int TravelSalesman(int i, int state, int[,] memo, int[,] prev)
        {

            // Done this tour. Return cost of going back to start node.
            if (state == FINISHED_STATE) return distance[i, START_NODE];

            // Return cached answer if already computed.
            if (memo[i, state] != 0) return memo[i, state];

            int minCost = int.MaxValue;
            int index = -1;
            for (int next = 0; next < N; next++)
            {

                // Skip if the next node has already been visited.
                if ((state & (1 << next)) != 0) continue;

                int nextState = state | (1 << next);
                int newCost = distance[i, next] + TravelSalesman(next, nextState, memo, prev);
                if (newCost < minCost)
                {
                    minCost = newCost;
                    index = next;
                }
            }

            prev[i, state] = index;
            return memo[i, state] = minCost;
        }

    }*/

    /*int minDistance(int[] dist, bool[] sptSet)
    {
        // Initialize min value 
        int min = int.MaxValue, min_index = -1;

        for (int v = 0; v < dist.Length; v++)
            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                min_index = v;
            }

        return min_index;
    }

    int DijkstraAlgorithm(int[,] graph, int src)
    {
        int size = graph.GetLength(0);
        int[] dist = Enumerable.Repeat(int.MaxValue, size).ToArray();
        bool[] sptSet = Enumerable.Repeat(false, size).ToArray();

        dist[src] = 0;

        for (int count = 0; count < size - 1; count++)
        {

            int u = minDistance(dist, sptSet);
            sptSet[u] = true;

            for (int v = 0; v < size; v++)
                if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    dist[v] = dist[u] + graph[u, v];
        }

        return dist.Max();
    }*/

}
