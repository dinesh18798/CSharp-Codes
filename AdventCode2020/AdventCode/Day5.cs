using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day5
    {
        public Day5()
        {
            string[] str = File.ReadAllLines(@"E:\boardingpass.txt");
            List<int> seatNum = new List<int>();
            Console.WriteLine(Part1(str, ref seatNum));
            Console.WriteLine(Part2(seatNum));
        }

        private int Part2(List<int> seatNum)
        {
            int num = 0;
            List<int> sortedSeat = seatNum.OrderBy(x => x).ToList();

            int min = sortedSeat[0];

            int max = sortedSeat[^1];

            for (; min < max && max > min; --max, ++min)
            {
                if (!sortedSeat.Contains(min))
                {
                    return min;
                }
                if (!sortedSeat.Contains(max))
                {
                    return max;
                }
            }

            return num;
        }

        int Part1(string[] input, ref List<int> seatNum)
        {
            int maxBoard = int.MinValue;

            foreach (string boardingPass in input)
            {
                double rowMin = 0; double rowMax = 127;
                double colMin = 0; double colMax = 7;

                string rowString = boardingPass.Substring(0, 7);

                foreach (char c in rowString[0..])
                {
                    if (c == 'F')
                    {
                        rowMax = Math.Floor((rowMin + rowMax) / 2);
                    }
                    else if (c == 'B')
                    {
                        rowMin = Math.Ceiling((rowMin + rowMax) / 2);
                    }
                }

                string colString = boardingPass[7..];

                foreach (char c in colString[0..])
                {
                    if (c == 'L')
                    {
                        colMax = Math.Floor((colMin + colMax) / 2);
                    }
                    else if (c == 'R')
                    {
                        colMin = Math.Ceiling((colMin + colMax) / 2);
                    }
                }

                int row = (int)(rowMin + rowMax) / 2;
                int col = (int)(colMin + colMax) / 2;

                int seat = row * 8 + col;
                seatNum.Add(seat);

                maxBoard = Math.Max(maxBoard, seat);
            }
            return maxBoard;
        }
    }
}
