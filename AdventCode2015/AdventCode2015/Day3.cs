using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2015
{
    public class Day3
    {

        public Day3()
        {
            string moves = File.ReadAllText(@"E:\GitHub\CSharp-Codes\AdventCode2015\moves.txt");

            Console.WriteLine($"Part 1: {Part1(moves)}");
            Console.WriteLine($"Part 2: {Part2(moves)}");
        }

        private int Part1(string moves)
        {
            HashSet<string> houseLoc = new HashSet<string>();
            int x = 0; int y = 0;
            houseLoc.Add(x.ToString() + y.ToString());
            foreach (char dir in moves)
            {
                switch (dir)
                {
                    case '>':
                        x += 1;
                        break;
                    case '<':
                        x -= 1;
                        break;
                    case 'v':
                        y -= 1;
                        break;
                    case '^':
                        y += 1;
                        break;
                    default:
                        break;
                }
                houseLoc.Add(x.ToString() + y.ToString());
            }

            return houseLoc.Count;
        }

        private int Part2(string moves)
        {
            List<string> santaHouseLoc = new List<string>();
            int santaX = 0; int santaY = 0;
            int roboX = 0; int roboY = 0;

            santaHouseLoc.Add(santaX.ToString() + santaY.ToString());

            bool isSanta = false;

            foreach (char dir in moves)
            {
                if (isSanta)
                {
                    switch (dir)
                    {
                        case '>':
                            santaX += 1;
                            break;
                        case '<':
                            santaX -= 1;
                            break;
                        case 'v':
                            santaY -= 1;
                            break;
                        case '^':
                            santaY += 1;
                            break;
                        default:
                            break;
                    }
                    santaHouseLoc.Add(santaX.ToString() + santaY.ToString());
                }
                else
                {
                    switch (dir)
                    {
                        case '>':
                            roboX += 1;
                            break;
                        case '<':
                            roboX -= 1;
                            break;
                        case 'v':
                            roboY -= 1;
                            break;
                        case '^':
                            roboY += 1;
                            break;
                        default:
                            break;
                    }
                    santaHouseLoc.Add(roboX.ToString() + roboY.ToString());
                }

                isSanta = !isSanta;
            }
            HashSet<string> final = new HashSet<string>(santaHouseLoc);
            return santaHouseLoc.Count; ;
        }
    }
}
