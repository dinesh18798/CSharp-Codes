using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            int f = (int)Math.Ceiling(10.05);


            /* char[][] board = {
                 new char[] { 's', 'o', 's', 'o' },
                 new char[] { 's', 'o', 'o', 'o' },
                 new char[] { 's', 's', 's', 's' }
             };
             string word = "sos";

             int count = findCountWord(board, word);

             Console.WriteLine($"sos Count: {count}");

             char[][] secondBoard = {
                 new char[] { 'a', 'a' },
                 new char[] { 'a', 'a' },
             };
             word = "aa";

             count = findCountWord(secondBoard, word);

             Console.WriteLine($"aa Count: {count}");
            */

            /* bool d = checkPalindrome("aabbaa");

             almostIncreasingSequence(new int[] { 1, 3, 2, 1 });

             firstDuplicate(new int[] { 2, 1, 3, 5, 3, 2 });*/

            /* int[][] board = {
                   new int[] { 1, 2, 3 },
                   new int[] { 4, 5, 6 },
                   new int[] { 7, 8, 9 }
             };

             rotateImage(board);*/

            /*
            LongestString longest = new LongestString();

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            string[] result = longest.allLongestStrings(new string[] { "young ", "yooooooung", "hot", "or", "not", "come", "on", "fire", "water", "watermelon" });

            foreach (string str in result)
            {
                Console.WriteLine(str);
            }
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


            watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            string[] resultLinq = longest.allLongestStringsLinq(new string[] { "young ", "yooooooung", "hot", "or", "not", "come", "on", "fire", "water", "watermelon" });

            foreach (string str in result)
            {
                Console.WriteLine(str);
            }

            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            CommonString comstr = new CommonString();

            //comstr.commonCharacterCount("aabcc", "adcaa");

            // comstr.isLucky(1230);

            // comstr.sortByHeight(new int[] { -1, 150, 190, 170, -1, -1, 160, 180 });

            Console.WriteLine($"The Value is { "foobazrabblim" == comstr.reverseInParentheses("foo(bar(baz))blim")} ");*/

            HashTables tables = new HashTables();

            string[][] dishes = new string[][]
            {
                new string[]{ "Salad", "Tomato", "Cucumber", "Salad", "Sauce" },
                new string[]{ "Pizza", "Tomato", "Sausage", "Sauce", "Dough" },
                new string[]{ "Quesadilla", "Chicken", "Cheese", "Sauce" },
                new string[]{"Sandwich", "Salad", "Bread", "Tomato", "Cheese" },

            };

            tables.groupingDishes(dishes);


            CountersClass countersClass = new CountersClass();
            countersClass.Solution(5, new int[] { 3, 4, 4, 6, 1, 4, 4 });

        }

        private static int findCountWord(char[][] board, string word)
        {
            int count = 0;

            //-- rows
            string tempWord = "";
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length - word.Length + 1; j++)
                {
                    int k = j;
                    while (k < word.Length + j)
                    {
                        char curr = board[i][k++];
                        tempWord += curr.ToString();
                    }

                    if (tempWord.Equals(word))
                        count++;
                    tempWord = "";
                }
            }

            //-- colums
            for (int i = 0; i < board[0].Length; i++)
            {
                for (int j = 0; j < board.Length - word.Length + 1; j++)
                {
                    int k = j;
                    while (k < word.Length + j)
                    {
                        char curr = board[k++][i];
                        tempWord += curr.ToString();
                    }

                    if (tempWord.Equals(word))
                        count++;
                    tempWord = "";
                }
            }

            //-- diagonals
            for (int row = 0; row < board.Length - word.Length + 1; row++)
            {
                for (int col = 0; col < board[0].Length - word.Length + 1; col++)
                {
                    int i = row; int j = col;
                    while (i < board.Length && j < board[0].Length)
                    {

                        char curr = board[i++][j++];
                        tempWord += curr.ToString();

                        if (tempWord.Length == word.Length)
                        {
                            if (tempWord.Equals(word))
                                count++;
                            tempWord = "";
                        }
                    }
                }
            }

            return count;
        }

        private static bool checkPalindrome(string inputString)
        {
            if (inputString.Length == 1) return true;

            // -- reverse the input string

            string reverseString = "";
            string str = inputString.Substring(0, (int)Math.Ceiling((double)inputString.Length / 2));
            for (int fromLast = inputString.Length - 1; fromLast >= inputString.Length / 2; fromLast--)
            {
                reverseString += inputString[fromLast];
            }

            //return inputString.SequenceEqual(inputString.Reverse());
            return str.SequenceEqual(inputString.Reverse());
        }

        static int adjacentElementsProduct(int[] inputArray)
        {
            int highest = int.MinValue;
            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                highest = Math.Max(highest, inputArray[i] * inputArray[i + 1]);
            }
            return highest;
        }

        static int shapeArea(int n)
        {
            return (int)(Math.Pow(n, 2) + Math.Pow(n - 1, 2));
        }

        static bool almostIncreasingSequence(int[] sequence)
        {
            int dropCount = 0;
            Tuple<int, int> dropPair = null;
            for (int i = 1; i < sequence.Length; i++)
            {
                if (sequence[i - 1] > sequence[i])
                {
                    dropCount++;
                    dropPair = new Tuple<int, int>(i - 1, i);

                }
                if (dropCount > 1)
                    return false;
            }

            if (dropPair != null)
            {
                int first = dropPair.Item1;
                int last = dropPair.Item2;
                if (first == 0 || last == sequence.Length - 1)
                    return true;

                bool start = sequence[first - 1] < sequence[last] && sequence[last] < sequence[last + 1];
                bool end = sequence[first - 1] < sequence[first] && sequence[first] < sequence[last + 1];


                if (!(start || end))
                    return false;
            }

            return true;

            /* for (int i = 1; i < seq.length; i++) 
                 if (seq[i] <= seq[i - 1])
                 {
                     bad++
          if (bad > 1) return false
          if (seq[i] <= seq[i - 2] && seq[i + 1] <= seq[i - 1]) return false*/

        }

        static int firstDuplicate(int[] a)
        {
            int duplicate = -1;
            Dictionary<int, int> existNumbers = new Dictionary<int, int>();

            for (int i = 0; i < a.Length; i++)
            {
                if (existNumbers.ContainsKey(a[i]))
                {
                    duplicate = a[i];
                    break;
                }
                else
                {
                    existNumbers.Add(a[i], 0);
                }
            }
            return duplicate;
        }

        char firstNotRepeatingCharacter(string s)
        {
            char currentFirstNon = '_';
            if (s.Length == 1) return s[0];

            List<char> repeatedCharacter = new List<char>();
            List<char> uniqueCharacter = new List<char>();
            foreach (char character in s[0..])
            {
                if (!uniqueCharacter.Contains(character) && !repeatedCharacter.Contains(character))
                {
                    uniqueCharacter.Add(character);
                }
                else
                {
                    repeatedCharacter.Add(character);
                    uniqueCharacter.Remove(character);
                }
            }

            if (uniqueCharacter.Count > 0)
                currentFirstNon = uniqueCharacter.ElementAt(0);

            return currentFirstNon;
        }


        static int[][] rotateImage(int[][] a)
        {
            int[][] result = new int[a.Length][];

            for (int i = 0; i < a[0].Length; i++)
            {
                int[] row = new int[a.Length];
                int position = row.Length - 1;
                for (int j = 0; j < a.Length; j++)
                {
                    if (position >= 0)
                    {
                        row[position--] = a[j][i];
                    }
                }

                result[i] = row;
            }

            return result;
        }

    }
}

