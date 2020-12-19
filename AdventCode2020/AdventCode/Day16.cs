using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2020
{
    public class Day16
    {
        struct Rule
        {
            int minA, maxA, minB, maxB;
            public int RuleID { get; set; }

            public Rule(int id, int mnA, int mxA, int mnB, int mxB)
            {
                minA = mnA;
                maxA = mxA;
                minB = mnB;
                maxB = mxB;
                RuleID = id;
            }

            internal bool IsValid(int ticketNumber)
            {
                return (minA <= ticketNumber && ticketNumber <= maxA) || (minB <= ticketNumber && ticketNumber <= maxB);
            }
        }

        Dictionary<string, Rule> rules;

        List<List<int>> tickets;
        List<List<int>> validTickets;

        public Day16()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\tickets.txt");

            int i = 0;
            // Add the rules
            rules = new Dictionary<string, Rule>();
            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;

                var groups = Regex.Match(inputs[i], "(\\w.*): (\\d+)-(\\d+) or (\\d+)-(\\d+)").Groups;
                Rule newRule = new Rule(i, int.Parse(groups[2].Value), int.Parse(groups[3].Value), int.Parse(groups[4].Value), int.Parse(groups[5].Value));

                rules.Add(groups[1].Value, newRule);
            }

            // Add your tickets
            i += 2;
            tickets = new List<List<int>>();
            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;

                tickets.Add(inputs[i].Split(',').Select(int.Parse).ToList());
            }

            // Add nearby tickets
            i += 2;
            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;

                tickets.Add(inputs[i].Split(',').Select(int.Parse).ToList());
            }

            validTickets = new List<List<int>>();

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 1: {Part2()}");
        }

        private long Part1()
        {
            long errRate = 0;
            HashSet<int> errNum = new HashSet<int>();
            HashSet<int> invalidTicketsID = new HashSet<int>();

            for (int i = 0; i < tickets.Count; i++)
            {
                List<int> currTicket = tickets[i];
                bool validTicket = true;
                foreach (int num in currTicket)
                {
                    bool valid = false;
                    if (!errNum.Contains(num))
                    {
                        foreach (Rule rule in rules.Values)
                        {
                            valid = rule.IsValid(num);
                            if (valid)
                                break;
                        }
                    }
                    if (!valid)
                    {
                        invalidTicketsID.Add(i);
                        errRate += num;
                        errNum.Add(num);
                        validTicket = false;
                    }
                }
                if (validTicket)
                {
                    validTickets.Add(currTicket);
                }
            }
            return errRate;
        }

        private long Part2()
        {
            Dictionary<int, int> rulesOrder = new Dictionary<int, int>();
            foreach (Rule currentRule in rules.Values)
            {
                bool match = true; int i = 0;
                for (; i < validTickets[0].Count; ++i)
                {
                    for (int j = 0; j < validTickets.Count; ++j)
                    {
                        match = currentRule.IsValid(validTickets[j][i]);
                        if (!match)
                            break;
                    }
                    if (match)
                    {
                        if (!rulesOrder.ContainsKey(i))
                        {
                            rulesOrder.Add(i, currentRule.RuleID);
                            break;
                        }
                    }
                }

            }

            return 0;
        }
    }
}
