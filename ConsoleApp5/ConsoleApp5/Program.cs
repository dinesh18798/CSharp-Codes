using System;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            int[] connection = new int[] { 0, 9, 0, 2, 6, 8, 0, 8, 3, 0 };
            /*Graph graph = new Graph(connection.Length);

            for (int i = 0; i < connection.Length; ++i)
            {
                graph.addEdge(connection[i], i);
            }

            graph.longestPathLength();*/

            LongestPathOdd longest = new LongestPathOdd();
            Console.WriteLine($"Path : {longest.longestPath(connection)}");

            Console.WriteLine($"Path : {longest.longestPath(new int[] { 0, 0, 0, 1, 6, 1, 0, 0 })}");
        }
    }
}
