using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day11
    {
        private enum Layout
        {
            EMPTY,
            OCCUPIED,
            FLOOR
        }

        List<List<Layout>> seatLayout;
        public Day11()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\seatsmap.txt");
            seatLayout = new List<List<Layout>>(inputs.Length + 2);

            List<Layout> tempList = Enumerable.Repeat(Layout.FLOOR, inputs[0].Length + 2).ToList();
            seatLayout.Add(tempList);

            for (int i = 0; i < inputs.Length; ++i)
            {
                char[] line = inputs[i].ToCharArray();

                tempList = new List<Layout>(line.Length + 2);
                tempList.Add(Layout.FLOOR);

                foreach (char c in line)
                {
                    if (c == 'L')
                        tempList.Add(Layout.EMPTY);
                    else if (c == '.')
                        tempList.Add(Layout.FLOOR);
                    else if (c == '#')
                        tempList.Add(Layout.OCCUPIED);
                }

                tempList.Add(Layout.FLOOR);
                seatLayout.Add(tempList);
            }

            tempList = Enumerable.Repeat(Layout.FLOOR, inputs[0].Length + 2).ToList();

            seatLayout.Add(tempList);

            Console.WriteLine($"Part 1: {Part(3, true)}");
            Console.WriteLine($"Part 2: {Part(4, false)}");
        }

        private int Part(int minSeats, bool adjacent)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            int occupiedSeats;
            List<List<Layout>> seats = new List<List<Layout>>(seatLayout);

            while (true)
            {
                List<List<Layout>> tempSeats = new List<List<Layout>>(seats.Count);
                List<Layout> tempList = Enumerable.Repeat(Layout.FLOOR, seats[0].Count).ToList();
                tempSeats.Add(tempList);

                occupiedSeats = 0;
                bool isChange = false;
                for (int i = 1; i < seats.Count - 1; ++i)
                {
                    tempList = new List<Layout>
                    {
                        Layout.FLOOR
                    };
                    for (int j = 1; j < seats[i].Count - 1; j++)
                    {
                        Layout currSeat = seats[i][j];
                        //If a seat is empty(L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                        if (currSeat == Layout.EMPTY)
                        {
                            int nearSeats = adjacent ? GetAdjacentOccupiedSeats(seats, i, j) : GetFirstOccupiedSeats(seats, i, j);
                            if (nearSeats == 0)
                            {
                                tempList.Add(Layout.OCCUPIED);
                                ++occupiedSeats;
                                isChange = true;
                            }
                            else
                            {
                                tempList.Add(currSeat);
                            }
                        }
                        //If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                        else if (currSeat == Layout.OCCUPIED)
                        {
                            int nearSeats = adjacent ? GetAdjacentOccupiedSeats(seats, i, j) : GetFirstOccupiedSeats(seats, i, j);
                            if (nearSeats > minSeats)
                            {
                                tempList.Add(Layout.EMPTY);
                                isChange = true;
                            }
                            else
                            {
                                tempList.Add(currSeat);
                                ++occupiedSeats;
                            }
                        }
                        else
                        {
                            tempList.Add(currSeat);
                        }
                    }
                    tempList.Add(Layout.FLOOR);
                    tempSeats.Add(tempList);
                }
                tempList = Enumerable.Repeat(Layout.FLOOR, seats[0].Count).ToList();
                tempSeats.Add(tempList);
                seats.Clear();
                seats.AddRange(tempSeats);

                if (!isChange)
                    break;
            }
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return occupiedSeats;
        }

        private int GetAdjacentOccupiedSeats(List<List<Layout>> seats, int x, int y)
        {
            int adjacentOccupiedSeats = 0;
            for (int i = x - 1; i <= x + 1; ++i)
            {
                for (int j = y - 1; j <= y + 1; ++j)
                {
                    if (i == x && j == y)
                        continue;
                    if (seats[i][j] == Layout.OCCUPIED)
                        ++adjacentOccupiedSeats;
                }
            }
            return adjacentOccupiedSeats;
        }

        private int GetFirstOccupiedSeats(List<List<Layout>> seats, int x, int y)
        {
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-1,0),
                new Tuple<int, int>(1,0),
                new Tuple<int, int>(0,-1),
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(-1,-1),
                new Tuple<int, int>(-1,1),
                new Tuple<int, int>(1,-1),
                new Tuple<int, int>(1,1)
            };

            int firstOccupiedSeats = 0;

            foreach (Tuple<int, int> position in positions)
            {
                int i = x + position.Item1;
                int j = y + position.Item2;
                while ((0 < i && i < seats.Count - 1) && (0 < j && j < seats[0].Count - 1))
                {
                    if (seats[i][j] == Layout.EMPTY)
                    {
                        break;
                    }
                    if (seats[i][j] == Layout.OCCUPIED)
                    {
                        ++firstOccupiedSeats;
                        break;
                    }

                    i += position.Item1;
                    j += position.Item2;
                }
            }
            return firstOccupiedSeats;
        }
    }

}
