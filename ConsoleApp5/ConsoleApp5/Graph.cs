using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    // Utility Pair class for storing 
    // maximum distance Node with its distance
    public class Pair<T, V>
    {
        // maximum distance Node
        public T first;

        // distance of maximum distance node
        public V second;

        // Constructor
        public Pair(T first, V second)
        {
            this.first = first;
            this.second = second;
        }
    }

    // This class represents a undirected graph 
    // using adjacency list 
    public class Graph
    {
        int V; // No. of vertices 
        List<int>[] adj; //Adjacency List 

        // Constructor 
        public Graph(int V)
        {
            this.V = V;

            // Initializing Adjacency List
            adj = new List<int>[V];
            for (int i = 0; i < V; ++i)
            {
                adj[i] = new List<int>();
            }
        }

        // function to add an edge to graph 
        public void addEdge(int s, int d)
        {
            adj[s].Add(d); // Add d to s's list. 
            //adj[d].Add(s); // Since the graph is undirected 
        }

        // method returns farthest node and 
        // its distance from node u 
        private Pair<int, int> bfs(int u)
        {
            int[] dis = new int[V];

            // mark all distance with -1 
            for (int i = 0; i < V; i++)
                dis[i] = -1;

            Queue<int> q = new Queue<int>();

            q.Enqueue(u);

            // distance of u from u will be 0 
            dis[u] = 0;
            while (q.Count != 0)
            {
                int t = q.Dequeue();

                // loop for all adjacent nodes of node-t 
                for (int i = 0; i < adj[t].Count; ++i)
                {
                    int v = adj[t][i];

                    // push node into queue only if 
                    // it is not visited already 
                    if (dis[v] == -1)
                    {
                        q.Enqueue(v);

                        // make distance of v, one more 
                        // than distance of t 
                        dis[v] = dis[t] + 1;
                    }
                }
            }
            int maxDis = 0;
            int nodeIdx = 0;

            // get farthest node distance and its index 
            for (int i = 0; i < V; ++i)
            {
                if (dis[i] > maxDis)
                {
                    maxDis = dis[i];
                    nodeIdx = i;
                }
            }
            return new Pair<int, int>(nodeIdx, maxDis);
        }

        // method prints longest path of given tree 
        public void longestPathLength()
        {
            Pair<int, int> t1, t2;

            // first bfs to find one end point of 
            // longest path 
            t1 = bfs(0);

            // second bfs to find actual longest path 
            t2 = bfs(t1.first);

            Console.WriteLine("longest path is from " + t1.first +
                    " to " + t2.first + " of length " + t2.second);
        }
    }
}
