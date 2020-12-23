using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day20
    {
        class Tile
        {
            private readonly int size;
            private readonly bool[][] cells;
            public long Id { get; private set; }
            public Orientation[] Orientations { get; private set; }

            public Tile(long id, bool[][] cells)
            {
                this.cells = cells;
                this.size = cells.Length;
                this.Id = id;
                this.Orientations = Enumerable.Range(0, 8).Select(i => new Orientation(this, GetOrientation(i))).ToArray();
            }

            private bool[][] GetOrientation(int value)
            {
                switch (value) // no particular order
                {
                    case 0: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[i][j]).ToArray()).ToArray();
                    case 1: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[j][i]).ToArray()).ToArray();
                    case 2: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[(size - 1) - i][j]).ToArray()).ToArray();
                    case 3: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[(size - 1) - j][i]).ToArray()).ToArray();
                    case 4: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[i][(size - 1) - j]).ToArray()).ToArray();
                    case 5: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[j][(size - 1) - i]).ToArray()).ToArray();
                    case 6: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[(size - 1) - i][(size - 1) - j]).ToArray()).ToArray();
                    case 7: return Enumerable.Range(0, size).Select(i => Enumerable.Range(0, size).Select(j => cells[(size - 1) - j][(size - 1) - i]).ToArray()).ToArray();
                }
                throw new Exception();
            }

            public class Orientation
            {
                public bool[][] Cells { get; private set; }
                public Tile Tile { get; private set; }
                public int North { get; private set; }
                public int South { get; private set; }
                public int East { get; private set; }
                public int West { get; private set; }

                public Orientation(Tile tile, bool[][] cells)
                {
                    this.Tile = tile;
                    this.Cells = cells;
                    for (var i = 0; i < 10; i++)
                    {
                        if (Cells[0][i]) North += 1;
                        if (Cells[9][i]) South += 1;
                        if (Cells[i][9]) East += 1;
                        if (Cells[i][0]) West += 1;
                        North <<= 1;
                        South <<= 1;
                        East <<= 1;
                        West <<= 1;
                    }
                }
            }
        }
        public Day20()
        {
            string[] inputLines = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\tiles.txt");
            var tiles = new List<Tile>();
            for (var i = 0; i < inputLines.Length; i += 12)
            {
                var id = int.Parse(inputLines[i].Substring(5, 4));
                var cells = Enumerable.Range(i + 1, 10).Select(j => inputLines[j].Select(c => c == '#').ToArray()).ToArray();
                tiles.Add(new Tile(id, cells));
            }

            Dictionary<(int x, int y), Tile.Orientation> places = null;
            foreach (var tile in tiles) // loop through tiles to start with in case one tile leads to a case where there is no unique choice (not in my input)
            {
                places = new Dictionary<(int x, int y), Tile.Orientation>();
                var orientation = tile.Orientations[0]; // doesn't matter which way around the first tile goes
                places.Add((0, 0), orientation);
                var remaining = tiles.Where(t => t != tile).ToList();
                while (remaining.Count > 0) // while we have tiles left to place
                {
                    bool matched = false;
                    var failed = new List<int[]>();
                    foreach (var connectTo in places.Keys) // find a tile to connect to - doesn't matter that we check internal edges since we're using uniqueness
                    {
                        var connectToOrientation = places[connectTo];
                        var north = remaining.SelectMany(t => t.Orientations).Where(o => o.South == connectToOrientation.North).ToArray();
                        if (north.Length == 1) // only connect a tile if it is the only possible choice
                        {
                            matched = true;
                            places.Add((connectTo.x, connectTo.y - 1), north[0]);
                            remaining.Remove(north[0].Tile);
                            break;
                        }
                        var south = remaining.SelectMany(t => t.Orientations).Where(o => o.North == connectToOrientation.South).ToArray();
                        if (south.Length == 1) // only connect a tile if it is the only possible choice
                        {
                            matched = true;
                            places.Add((connectTo.x, connectTo.y + 1), south[0]);
                            remaining.Remove(south[0].Tile);
                            break;
                        }
                        var east = remaining.SelectMany(t => t.Orientations).Where(o => o.West == connectToOrientation.East).ToArray();
                        if (east.Length == 1) // only connect a tile if it is the only possible choice
                        {
                            matched = true;
                            places.Add((connectTo.x + 1, connectTo.y), east[0]);
                            remaining.Remove(east[0].Tile);
                            break;
                        }
                        var west = remaining.SelectMany(t => t.Orientations).Where(o => o.East == connectToOrientation.West).ToArray();
                        if (west.Length == 1) // only connect a tile if it is the only possible choice
                        {
                            matched = true;
                            places.Add((connectTo.x - 1, connectTo.y), west[0]);
                            remaining.Remove(west[0].Tile);
                            break;
                        }
                        failed.Add(new[] { north.Length, south.Length, east.Length, west.Length });
                    }
                    if (!matched) break; // if we ever can't uniquely connect a tile, bail out (not in my input)
                }
                if (remaining.Count == 0) break; // we placed all tiles successfully
            }

            // we won't be centered on zero so for now use mins and maxes
            var minX = places.Keys.Min(p => p.x);
            var minY = places.Keys.Min(p => p.y);
            var maxX = places.Keys.Max(p => p.x);
            var maxY = places.Keys.Max(p => p.y);

            for (int i = minX; i <= maxX; ++i)
            {
                for (int j = minY; j <= maxY; ++j)
                {
                    Console.Write($"{places[(i, j)].Tile.Id} ");
                }
                Console.WriteLine('\n');
            }

            Console.WriteLine($"{places[(minX, minY)].Tile.Id} {places[(minX, maxY)].Tile.Id} {places[(maxX, minY)].Tile.Id} {places[(maxX, maxY)].Tile.Id}");

            Console.WriteLine($"Part 1: {places[(minX, minY)].Tile.Id * places[(minX, maxY)].Tile.Id * places[(maxX, minY)].Tile.Id * places[(maxX, maxY)].Tile.Id}");

            // build our tiles into one big grid. keep track of choppy tile count
            var chops = 0;
            var points = Enumerable.Range(0, 8 * (maxY - minY + 1)).Select(q1 => Enumerable.Range(0, 8 * (maxX - minX + 1)).Select(q2 => false).ToArray()).ToArray();
            foreach (var place in places)
                for (var x = 1; x < 9; x++)
                    for (var y = 1; y < 9; y++)
                        if (place.Value.Cells[y][x])
                        {
                            points[(place.Key.y - minY) * 8 + y - 1][(place.Key.x - minX) * 8 + x - 1] = true;
                            chops++;
                        }

            var masterTile = new Tile(-1, points); // this lets us reuse the "orientations" code for free

            var pattern = new[] // can't be bothered turning this into anything else
            {
        "                  # ",
        "#    ##    ##    ###",
        " #  #  #  #  #  #   "
    };
            foreach (var masterOrientation in masterTile.Orientations)
            {
                var monsters = 0;
                for (var i = 0; i < masterOrientation.Cells.Length - pattern.Length; i++) // simple scan through rows and columns
                {
                    for (var j = 0; j < masterOrientation.Cells[0].Length - pattern[0].Length; j++)
                    {
                        var monster = true;
                        for (var di = 0; di < pattern.Length; di++)
                        {
                            for (var dj = 0; dj < pattern[0].Length; dj++)
                            {
                                if (pattern[di][dj] == '#' && !masterOrientation.Cells[i + di][j + dj])
                                {
                                    monster = false;
                                    break;
                                }
                            }
                            if (!monster) break;
                        }

                        if (monster)
                        {
                            monsters++;
                        }
                    }
                }

                if (monsters > 0)
                {

                    Console.WriteLine($"Part 2: {chops - (monsters * 15)}"); // continue to check other orientations as a sanity check; should only get one output
                }
            }
        }
    }
}
