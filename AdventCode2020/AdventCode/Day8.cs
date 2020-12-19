using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class InstructionOperation
    {
        public InstructionOperation(string oprt, int argm)
        {
            operation = oprt;
            argument = argm;
        }
        public string operation;
        public int argument;
    }

    public class Result
    {
        public int accumulator = 0;
        public bool isLooping = false;
    }

    public class Day8
    {
        string[] instructions;


        public Day8()
        {
            instructions = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode\instruction.txt");
            List<InstructionOperation> instructionsMap = new List<InstructionOperation>(instructions.Length);

            foreach (string instruct in instructions)
            {
                string[] temp = instruct.Split(' ');
                instructionsMap.Add(new InstructionOperation(temp[0], int.Parse(temp[1])));
            }

            Console.WriteLine($"Part 1 : {OperationAcc(new List<InstructionOperation>(instructionsMap)).accumulator}");
            Console.WriteLine($"Part 2 : {Part2(new List<InstructionOperation>(instructionsMap)).accumulator}");
        }

        private Result Part2(List<InstructionOperation> instructionsMap)
        {
            Result result;
            int index = 0;
            string oldIns = string.Empty;
            do
            {
                bool found = false;
                while (!found && index < instructionsMap.Count)
                {
                    InstructionOperation currIns = instructionsMap[index];

                    if (currIns.operation == "jmp")
                    {
                        found = true;
                        oldIns = currIns.operation;
                        currIns.operation = "nop";
                    }
                    else if (currIns.operation == "nop")
                    {
                        found = true;
                        oldIns = currIns.operation;
                        currIns.operation = "jmp";
                    }
                    ++index;
                }

                result = OperationAcc(instructionsMap);
                if (result.isLooping)
                {
                    instructionsMap[index - 1].operation = oldIns;
                }
                else
                {
                    break;
                }
            } while (index < instructionsMap.Count);

            return result;
        }

        private Result OperationAcc(List<InstructionOperation> instructionsMap)
        {
            Result result = new Result();
            int index = 0;

            InstructionOperation currOperation = instructionsMap[index];
            List<int> visitedOperation = new List<int>();

            while (!visitedOperation.Contains(index))
            {
                visitedOperation.Add(index);
                switch (currOperation.operation)
                {
                    case "nop":
                        ++index;
                        break;
                    case "jmp":
                        index += currOperation.argument;
                        break;
                    case "acc":
                        result.accumulator += currOperation.argument;
                        ++index;
                        break;
                    default:
                        break;
                }
                if (index >= instructionsMap.Count)
                    break;
                currOperation = instructionsMap[index];
            }

            if (visitedOperation.Contains(index))
            {
                result.isLooping = true;
            }

            return result;
        }
    }
}
