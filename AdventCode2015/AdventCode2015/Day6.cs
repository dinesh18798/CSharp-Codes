using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    public struct Instruction
    {
        public string instruct;
        public int startRow;
        public int endRow;
        public int startColumn;
        public int endColumn;

        public Instruction(string ins, string sR, string sC, string eR, string eC)
        {
            instruct = ins;
            startRow = Math.Min(int.Parse(sR), int.Parse(eR));
            startColumn = Math.Min(int.Parse(sC), int.Parse(eC));
            endRow = Math.Max(int.Parse(sR), int.Parse(eR));
            endColumn = Math.Max(int.Parse(sC), int.Parse(eC));
        }
    }

    public class Day6
    {
        List<Instruction> instructions;
        string[] inputs;
        public Day6()
        {
            Regex regex = new Regex("(\\w.*) (\\d+),(\\d+) through (\\d+),(\\d+)");
            instructions = new List<Instruction>();
            inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\lights.txt");

            foreach (string input in inputs)
            {
                GroupCollection tempGroup = regex.Match(input).Groups;

                Instruction instruction = new Instruction(tempGroup[1].Value, tempGroup[2].Value, tempGroup[3].Value,
                    tempGroup[4].Value, tempGroup[5].Value);
                instructions.Add(instruction);
            }

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        private int Part1()
        {
            int onLight = 0;
            bool[,] lights = new bool[1000, 1000];

            foreach (Instruction instruction in instructions)
            {
                for (int x = instruction.startRow; x < instruction.endRow + 1; ++x)
                {
                    for (int y = instruction.startColumn; y < instruction.endColumn + 1; ++y)
                    {
                        switch (instruction.instruct)
                        {
                            case "turn on":
                                lights[x, y] = true;
                                break;
                            case "turn off":
                                lights[x, y] = false;
                                break;
                            case "toggle":
                                lights[x, y] = !lights[x, y];
                                break;
                        }
                    }
                }
            }

            foreach (bool light in lights)
            {
                if (light)
                    ++onLight;
            }

            return onLight;
        }


        private long Part2()
        {
            long totalLight = 0;
            int[,] lights = new int[1000, 1000];

            foreach (Instruction instruction in instructions)
            {
                for (int x = instruction.startRow; x < instruction.endRow + 1; ++x)
                {
                    for (int y = instruction.startColumn; y < instruction.endColumn + 1; ++y)
                    {
                        switch (instruction.instruct)
                        {
                            case "turn on":
                                lights[x, y] += 1;
                                break;
                            case "turn off":
                                if (lights[x, y] > 0)
                                    lights[x, y] -= 1;
                                break;
                            case "toggle":
                                lights[x, y] += 2;
                                break;
                        }
                    }
                }
            }

            foreach (int light in lights)
            {
                totalLight += light;
            }

            return totalLight;
        }
    }
}
