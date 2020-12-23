using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode2020
{
    public class Day23
    {
        public class CupNode
        {
            public int value;
            public CupNode nextNode;

            public CupNode(int v)
            {
                value = v;
            }

            internal (CupNode beginPickUpNode, CupNode middlePickUpNode, CupNode endPickUpNode) PickUp()
            {
                CupNode begin = nextNode;
                CupNode middle = begin.nextNode;
                CupNode end = middle.nextNode;
                nextNode = end.nextNode;
                end.nextNode = null;

                return (begin, middle, end);
            }
        }

        List<int> cups;
        public Day23()
        {
            cups = ("653427918").ToCharArray().Select(x => Convert.ToInt32(char.GetNumericValue(x))).ToList();
            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        string Part1()
        {
            List<int> tempCups = new List<int>(cups);

            Dictionary<int, CupNode> cupNodes = new Dictionary<int, CupNode>();
            CupNode prevNode = null;
            for (int i = tempCups.Count - 1; i >= 0; --i)
            {
                CupNode newNode = new CupNode(tempCups[i])
                {
                    nextNode = prevNode
                };
                cupNodes[tempCups[i]] = newNode;
                prevNode = newNode;
            }

            cupNodes = cupNodes.Reverse().ToDictionary(x => x.Key, x => x.Value);
            cupNodes.Last().Value.nextNode = cupNodes.First().Value;

            Moves(cupNodes, 100);

            string res = string.Empty;

            CupNode currentNode = cupNodes[1].nextNode;
            while (currentNode.value != 1)
            {
                res += currentNode.value.ToString();
                currentNode = currentNode.nextNode;
            }
            return res;
        }

        /* private List<int> Moves(List<int> tempCups)
         {
             int currentCup = tempCups[0];
             List<int> pickUpCups = tempCups.GetRange(1, 3);
             int startIndex = 4;
             List<int> remainingCups = tempCups.GetRange(startIndex, tempCups.Count - startIndex);

             int destination = currentCup - 1;
             int destIndex = -1;

             while (destination > 0)
             {
                 if (remainingCups.Contains(destination))
                 {
                     destIndex = tempCups.IndexOf(destination);
                     break;
                 }
                 --destination;
             }
             destination = destination == 0 ? remainingCups.Max() : destination;
             destIndex = destIndex == -1 ? tempCups.IndexOf(destination) : destIndex;
             remainingCups.RemoveRange(0, remainingCups.IndexOf(destination) + 1);

             List<int> result = new List<int>(tempCups.GetRange(startIndex, destIndex - startIndex + 1));
             result.AddRange(pickUpCups);
             result.AddRange(remainingCups);
             result.Add(currentCup);

             return result;
         }*/

        string Part2()
        {
            List<int> tempCups = new List<int>(cups);
            tempCups.AddRange(Enumerable.Range(10, 1_000_000 - tempCups.Count));

            Dictionary<int, CupNode> cupNodes = new Dictionary<int, CupNode>();
            CupNode prevNode = null;
            for (int i = tempCups.Count - 1; i >= 0; --i)
            {
                CupNode newNode = new CupNode(tempCups[i])
                {
                    nextNode = prevNode
                };           
                cupNodes[tempCups[i]] = newNode;
                prevNode = newNode;
            }

            cupNodes = cupNodes.Reverse().ToDictionary(x => x.Key, x => x.Value);
            cupNodes.Last().Value.nextNode = cupNodes.First().Value;

            Moves(cupNodes, 10_000_000);

            long first = cupNodes[1].nextNode.value;
            long second = cupNodes[1].nextNode.nextNode.value;

            return (first * second).ToString();
        }

        void Moves(Dictionary<int, CupNode> cupNodes, int moves)
        {
            CupNode currentCup = cupNodes.First().Value;

            for (int r = 0; r < moves; ++r)
            {
                (CupNode beginPickUpNode, CupNode middlePickUpNode, CupNode endPickUpNode) = currentCup.PickUp();
                HashSet<int> pickUpValues = new HashSet<int>()
                {   beginPickUpNode.value,
                    middlePickUpNode.value,
                    endPickUpNode.value
                };

                int dest = currentCup.value - 1 > 0 ? currentCup.value - 1 : cupNodes.Count;
                while (pickUpValues.Contains(dest))
                {
                    --dest;
                    if (dest == 0)
                        dest = cupNodes.Count;
                }

                CupNode destCupNode = cupNodes[dest];
                CupNode afterDestCupNode = destCupNode.nextNode;
                destCupNode.nextNode = beginPickUpNode;
                endPickUpNode.nextNode = afterDestCupNode;

                currentCup = currentCup.nextNode;
            }
        }
    }
}
