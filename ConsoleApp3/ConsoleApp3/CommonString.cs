using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp3
{
    public class CommonString
    {
        public int commonCharacterCount(string s1, string s2)
        {
            List<char> tempStr = s2.ToList();

            foreach (char c in s1)
            {
                List<char> temp = tempStr;
                if (temp.Contains(c))
                {
                    tempStr.Remove(c);
                }
            }

            return s2.Length - tempStr.Count;
        }

        internal bool isLucky(int n)
        {

            string num = n.ToString();

            int middle = num.Length / 2;

            int firstHalf = num.Substring(0, middle).Sum(x => x - '0');
            int secondHalf = num.Substring(middle).Sum(x => x - '0');

            return firstHalf == secondHalf;

        }

        public int[] sortByHeight(int[] a)
        {
            List<int> realHeights = a.Where(h => h != -1).OrderBy(h => h).ToList();

            for (int i = 0; i < a.Length; i++)
            {
                int currHeight = a[i];

                if (currHeight > -1)
                {
                    a[i] = realHeights[0];
                    realHeights.RemoveAt(0);
                }
            }
            return a;
        }

        public string reverseInParentheses(string inputString)
        {
            Stack<int> charStack = new Stack<int>();

            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == '(')
                {
                    charStack.Push(i);
                }
                else if (inputString[i] == ')')
                {
                    string temp = new string(inputString.Substring(charStack.Peek() + 1, i - charStack.Peek() - 1).Reverse().ToArray());
                    inputString = inputString.Remove(charStack.Peek() + 1, i - charStack.Peek() - 1).Insert(charStack.Peek() + 1, temp);

                    charStack.Pop();
                }
            }

            return new string(inputString.Where(c => c != '(' && c != ')').ToArray());
        }
    }
}
