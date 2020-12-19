using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day17
    {
        Dictionary<(int x, int y, int z, int w), char> originalCells;
        public Day17()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\energy.txt");

            originalCells = new Dictionary<(int x, int y, int z, int w), char>();

            for (int y = 0; y < inputs.Length; ++y)
            {
                for (int x = 0; x < inputs[y].Length; ++x)
                {
                    originalCells[(x, y, 0, 0)] = inputs[y][x];
                }
            }
            Console.WriteLine($"Part 1: {new Cells(originalCells, false).ActiveCells}");
            Console.WriteLine($"Part 2: {new Cells(originalCells, true).ActiveCells}");
        }

        internal class Cells
        {
            Dictionary<(int x, int y, int z, int w), char> cells;

            public bool Is4D { get; set; } = false;

            public int ActiveCells { get { return cells.Count(cell => cell.Value == '#'); } }

            public int Cycles { get; set; } = 6;

            internal Cells(Dictionary<(int x, int y, int z, int w), char> inputCells, bool fourD, int cyc = 6)
            {
                cells = new Dictionary<(int x, int y, int z, int w), char>(inputCells);
                Is4D = fourD;

                for (int i = 0; i < Cycles; ++i)
                {
                    MakeCycle();
                }
            }

            internal void MakeCycle()
            {
                Dictionary<(int x, int y, int z, int w), char> newCells = new Dictionary<(int x, int y, int z, int w), char>();

                foreach (var coordinate in new HashSet<(int, int, int, int)>(cells.SelectMany(c => GetNeighbourCells(c.Key))))
                {
                    int activeNeighbour = GetNeighbourCells(coordinate).Select(nCell =>
                    {
                        if (cells.ContainsKey(nCell))
                        {
                            return cells[nCell];
                        }
                        else
                            return '.';
                    }).Count(state => state == '#');

                    if (activeNeighbour == 0)
                        continue;

                    char currentState = cells.ContainsKey(coordinate) ? cells[coordinate] : '.';

                    if (currentState == '#')
                    {
                        if (activeNeighbour < 2 || activeNeighbour > 3)
                        {
                            currentState = '.';
                        }
                    }
                    else if (currentState == '.')
                    {
                        if (activeNeighbour == 3)
                        {
                            currentState = '#';
                        }
                    }
                    newCells[coordinate] = currentState;
                }
                cells = newCells;
            }

            private HashSet<(int x, int y, int z, int w)> GetNeighbourCells((int x, int y, int z, int w) coordinate)
            {
                HashSet<(int x, int y, int z, int w)> neighbourCells = new HashSet<(int x, int y, int z, int w)>();

                for (int z = coordinate.z - 1; z <= coordinate.z + 1; ++z)
                {
                    for (int y = coordinate.y - 1; y <= coordinate.y + 1; ++y)
                    {
                        for (int x = coordinate.x - 1; x <= coordinate.x + 1; ++x)
                        {
                            if (Is4D)
                            {
                                for (int w = coordinate.w - 1; w <= coordinate.w + 1; ++w)
                                {
                                    if ((x, y, z, w) != coordinate)
                                    {
                                        neighbourCells.Add((x, y, z, w));
                                    }
                                }
                            }
                            else
                            {
                                if ((x, y, z, 0) != coordinate)
                                {
                                    neighbourCells.Add((x, y, z, 0));
                                }
                            }
                        }
                    }
                }
                return neighbourCells;
            }
        }
    }
}
