using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2015
{
    struct Dimension
    {
        public int l;
        public int w;
        public int h;

        public Dimension(int len, int wid, int hgt)
        {
            l = len;
            w = wid;
            h = hgt;
        }
    }

    public class Day2
    {
        List<Dimension> dimensionsList;
        public Day2()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\dimensions.txt");
            dimensionsList = new List<Dimension>(inputs.Length);

            foreach (string input in inputs)
            {
                int[] dimension = input.Split('x').Select(int.Parse).ToArray();
                dimensionsList.Add(new Dimension(dimension[0], dimension[1], dimension[2]));
            }

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private int Part2()
        {
            int ribbon = 0;

            foreach (Dimension dimension in dimensionsList)
            {
                int length = 2 * (Math.Min(dimension.l + dimension.w, Math.Min(dimension.w + dimension.h, dimension.l + dimension.h)));
                length += dimension.l * dimension.w* dimension.h;

                ribbon += length;
            }

            return ribbon;
        }

        private int Part1()
        {
            int totalSurface = 0;

            foreach (Dimension dimension in dimensionsList)
            {
                int lw = dimension.l * dimension.w;
                int wh = dimension.w * dimension.h;
                int hl = dimension.h * dimension.l;
                int tempArea = (2 * lw) + (2 * wh) + (2 * hl);
                tempArea += Math.Min(lw, Math.Min(wh, hl));

                totalSurface += tempArea;
            }

            return totalSurface;
        }
    }
}
