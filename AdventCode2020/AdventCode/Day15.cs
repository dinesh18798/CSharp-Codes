using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode2020
{
    public class Day15
    {
        Dictionary<int, int> numbers;


        public Day15()
        {
            string[] inputs = ("1,0,18,10,19,6").Split(',');
            numbers = new Dictionary<int, int>();
            int count = 1;

            foreach (string item in inputs)
            {
                numbers.Add(int.Parse(item), count);
                ++count;
            }

            Console.WriteLine($"Part 1: {Part(numbers, 2020)}");
            Console.WriteLine($"Part 2: {Part(numbers, 30000000)}");
        }

        private int Part(Dictionary<int, int> nums, int maxTurn)
        {
            Dictionary<int, int> numbers = new Dictionary<int, int>(nums);

            int currTurn = numbers.Count;
            int currNumber = numbers.Keys.Last();
            numbers.Remove(currNumber);

            for (; currTurn < maxTurn; ++currTurn)
            {
                if(numbers.ContainsKey(currNumber))
                {
                    int prevTurn = numbers[currNumber];
                    numbers[currNumber] = currTurn;
                    currNumber = currTurn - prevTurn;
                }
                else
                {
                    numbers[currNumber] = currTurn;
                    currNumber = 0;
                }
            }

            return currNumber;
        }
    }
}
