using System;
using System.Collections.Generic;

namespace ConsoleApp6
{

    struct NumberValue : IEquatable<int>
    {
        int X;
        public bool Equals(int other)
        {
            return X == other;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //object player = new Player(new List<string>());
            NumberValue player = new NumberValue();

            bool test1 = IsZero1(player);
            bool test2 = IsZero2(player);
            bool test3 = IsZero3<object>(player);
            bool test4 = IsZero4(player);
            /*int t = 2 ^ 2;
            int N = int.Parse(Console.ReadLine());
            Dictionary<int, int> orderDict = new Dictionary<int, int>();
            for (int i = 0; i < N; i++)
            {
                int h = int.Parse(Console.ReadLine());

                foreach (int key in orderDict.Keys)
                {
                    if (key < h)
                        orderDict.Remove(key);
                }
                orderDict.Add(h, i);
            }

            foreach (int k in orderDict.Keys)
            {
                Console.WriteLine($"{k}[{orderDict[k]}]");
            }*/

            //Spoon(1, 3, new string[] { "0.0.0" });
        }

        static bool IsZero1(object x)
        {
            return x.Equals(0);
        }
        static bool IsZero2(IEquatable<int> x)
        {
            return x.Equals(0);
        }
        static bool IsZero3<T>(T x)
        {
            return x.Equals(0);
        }
        static bool IsZero4<T>(T x) where T : IEquatable<int>
        {
            return x.Equals(0);
        }

        static void RemoveDuplicates(List<int> myList)
        {
            int listCount = myList.Count;
            List<int> temp = new List<int>(listCount);
            for (int i = 0; i < listCount; i += 1)
            {
                if (!temp.Contains(myList[i]))
                {
                    temp.Add(myList[i]);
                }
            }
            myList.Clear();
            myList.AddRange(temp);
        }

        static void Spoon(int he, int wi, string[] lines)
        {
            int width = wi; // the number of cells on the X axis
            int height = he; // the number of cells on the Y axis

            List<List<int>> cells = new List<List<int>>();

            for (int i = 0; i < height; i++)
            {
                char[] line = lines[i].ToCharArray();
                List<int> tempCell = new List<int>();
                foreach (char c in line)
                {
                    if (c == '0')
                    {
                        tempCell.Add(1);
                    }
                    else
                    {
                        tempCell.Add(-1);
                    }
                }
                cells.Add(tempCell);
            }

            for (int h = 0; h < height; ++h)
            {
                for (int w = 0; w < width; ++w)
                {
                    int curr = cells[h][w];
                    if (curr == 1)
                    {
                        string res = string.Format("{0} {1}", w, h);
                        int toRight = w + 1;
                        int right = -1;
                        while (toRight < width && right != 1)
                        {
                            right = cells[h][toRight];
                            if (right == 1)
                                break;
                            ++toRight;
                        }
                        res += right == 1 ? string.Format(" {0} {1}", toRight, h) : " -1 -1";

                        int bottom = -1;
                        int toBottom = h + 1;
                        while (toBottom < height && bottom != 1)
                        {
                            bottom = cells[toBottom][w];
                            if (bottom == 1)
                                break;
                            ++toBottom;
                        }
                        res += bottom == 1 ? string.Format(" {0} {1}", w, toBottom) : " -1 -1";

                        Console.WriteLine(2);
                    }
                }
            }
        }
    }
}
