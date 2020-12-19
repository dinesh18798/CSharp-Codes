using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCode2015
{
    public class Day7
    {
        public enum Operation
        {
            NULL,
            STORE,
            AND,
            OR,
            LSHIFT,
            RSHIFT,
            NOT
        }

        private Operation GetOperation(string value)
        {
            return value switch
            {
                "AND" => Operation.AND,
                "OR" => Operation.OR,
                "LSHIFT" => Operation.LSHIFT,
                "RSHIFT" => Operation.RSHIFT,
                _ => Operation.NULL,
            };
        }

        public class Gate
        {
            public string wire1;
            public string wire2;
            public Operation operation;
            public ushort signal;
            public bool isMemoized;

            public Gate(string l) : this(l, string.Empty, Operation.NOT) { }
            public Gate(string l, string r, Operation op, ushort opt = 0, bool mem = false)
            {
                wire1 = l;
                wire2 = r;
                operation = op;
                signal = opt;
                isMemoized = mem;
            }
        }

        Dictionary<string, Gate> circuits;
        public Day7()
        {
            string[] texts = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2015\kits.txt");
            circuits = new Dictionary<string, Gate>();

            Regex varRegex = new Regex("^(\\w+) -> (\\w+)$");
            Regex notRegex = new Regex("^NOT (\\w+) -> (\\w+)$");
            Regex opRegex = new Regex("^(\\w+) (AND|OR|LSHIFT|RSHIFT) (\\w+) -> (\\w+)$");

            foreach (var input in texts)
            {
                if (varRegex.IsMatch(input))
                {
                    GroupCollection groups = varRegex.Match(input).Groups;
                    circuits.Add(groups[2].Value, new Gate(groups[1].Value, string.Empty, Operation.STORE));
                }
                else if (opRegex.IsMatch(input))
                {
                    GroupCollection groups = opRegex.Match(input).Groups;
                    circuits.Add(groups[4].Value, new Gate(groups[1].Value, groups[3].Value, GetOperation(groups[2].Value)));
                }
                else if (notRegex.IsMatch(input))
                {
                    GroupCollection groups = notRegex.Match(input).Groups;
                    circuits.Add(groups[2].Value, new Gate(groups[1].Value));
                }
            }
            ushort outputA = FindWireOutput("a");
            Console.WriteLine($"Part 1: {outputA}");

            ResetCircuit();

            if(circuits.ContainsKey("b"))
            {
                circuits["b"].signal = outputA;
                circuits["b"].operation = Operation.NULL;
                circuits["b"].isMemoized = true;
            }

            outputA = FindWireOutput("a");
            Console.WriteLine($"Part 2: {outputA}");

        }

        private ushort FindWireOutput(string wire)
        {
            if(ushort.TryParse(wire, out ushort output))
            {
                return output;
            }
            else
            {
               return GetOutput(wire);
            }

        }

        private void ResetCircuit()
        {
            foreach (Gate gate in circuits.Values)
            {
                gate.isMemoized = false;
            }
        }

        private ushort GetOutput(string wire)
        {

            if (circuits[wire].isMemoized)
                return circuits[wire].signal;
            else
            {
                switch (circuits[wire].operation)
                {
                    case Operation.STORE:
                        circuits[wire].signal = FindWireOutput(circuits[wire].wire1);
                        break;
                    case Operation.AND:
                        circuits[wire].signal = (ushort)(FindWireOutput(circuits[wire].wire1) & FindWireOutput(circuits[wire].wire2));
                        break;
                    case Operation.OR:
                        circuits[wire].signal = (ushort)(FindWireOutput(circuits[wire].wire1) | FindWireOutput(circuits[wire].wire2));
                        break;
                    case Operation.LSHIFT:
                        circuits[wire].signal = (ushort)(FindWireOutput(circuits[wire].wire1) << FindWireOutput(circuits[wire].wire2));
                        break;
                    case Operation.RSHIFT:
                        circuits[wire].signal = (ushort)(FindWireOutput(circuits[wire].wire1) >> FindWireOutput(circuits[wire].wire2));
                        break;
                    case Operation.NOT:
                        circuits[wire].signal = (ushort)~FindWireOutput(circuits[wire].wire1);
                        break;
                    default:
                        break;
                }
            }
            circuits[wire].isMemoized = true;
            return circuits[wire].signal;
        }
    }
}
