using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day24
    {
        Dictionary<string, (int x, int y)> tilesDirection = new Dictionary<string, (int x, int y)>
        {
            { "e", (2, 0) },
            { "se", (1, -1) },
            { "sw", (-1, -1) },
            { "w", (-2, 0) },
            { "nw", (-1, 1) },
            { "ne", (1, 1) }
        };

        Dictionary<int, List<(int x, int y)>> tilesMap;

        public Day24()
        {
            tilesMap = new Dictionary<int, List<(int x, int y)>>();
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\hextiles.txt");

            for (int i = 0; i < inputs.Length; ++i)
            {
                string currInput = inputs[i];
                List<(int x, int y)> tempIns = new List<(int x, int y)>();
                for (int s = 0; s < currInput.Length; s++)
                {
                    if (currInput[s] == 'e' || currInput[s] == 'w')
                    {
                        tempIns.Add(tilesDirection[currInput.Substring(s, 1)]);
                    }
                    else if (currInput[s] == 'n' || currInput[s] == 's')
                    {
                        tempIns.Add(tilesDirection[currInput.Substring(s, 2)]);
                        ++s;
                    }
                }
                tilesMap.Add(i, tempIns);
            }
            HashSet<(int x, int y)> blackTileMap = Part1();
            Console.WriteLine($"Part 1: {blackTileMap.Count}");
            Console.WriteLine($"Part 2: {Part2(blackTileMap)}");
        }

        private int Part2(HashSet<(int x, int y)> tileMap)
        {
            HashSet<(int x, int y)> blackTileMap = tileMap;
            for (int i = 0; i < 100; i++)
            {
                Dictionary<(int x, int y), int> allNeighbours = new Dictionary<(int x, int y), int>();
                foreach (var currBlackTile in blackTileMap)
                {
                    if (!allNeighbours.ContainsKey(currBlackTile))
                        allNeighbours[currBlackTile] = 0;
                    foreach (var neighbour in GetNeighbours(currBlackTile))
                    {
                        if (allNeighbours.ContainsKey(neighbour))
                            ++allNeighbours[neighbour];
                        else
                            allNeighbours[neighbour] = 1;
                    }
                }
                HashSet<(int x, int y)> newblackTileMap = new HashSet<(int x, int y)>();

                foreach (var currTile in allNeighbours)
                {
                    if (blackTileMap.Contains(currTile.Key) && currTile.Value > 0 && currTile.Value <= 2)
                        newblackTileMap.Add(currTile.Key);
                    else if(!blackTileMap.Contains(currTile.Key) && currTile.Value == 2)
                        newblackTileMap.Add(currTile.Key);
                }
                blackTileMap = newblackTileMap;
            }

            return blackTileMap.Count;
        }

        IEnumerable<(int x, int y)> GetNeighbours((int x, int y) currTile)
        {
            return tilesDirection.Values.Select(c => (c.x + currTile.x, c.y + currTile.y));
        }


        private HashSet<(int x, int y)> Part1()
        {
            HashSet<(int x, int y)> tiles = new HashSet<(int x, int y)>();

            foreach (List<(int x, int y)> currMap in tilesMap.Values)
            {
                (int x, int y) coord = (0, 0);
                foreach (var currDirection in currMap)
                {
                    coord.x += currDirection.x;
                    coord.y += currDirection.y;
                }
                if (tiles.Contains(coord))
                    tiles.Remove(coord);
                else
                    tiles.Add(coord);
            }

            return tiles;
        }
    }
}
