using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day22
    {
        List<int> player1;
        List<int> player2;

        public Day22()
        {
            string[] inputs = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\cards.txt");

            int i = 0;
            player1 = new List<int>();
            for (; i < inputs.Length; ++i)
            {
                if (string.IsNullOrEmpty(inputs[i]))
                    break;
                if (int.TryParse(inputs[i], out int num))
                    player1.Add(num);
            }

            i += 2;
            player2 = new List<int>();
            for (; i < inputs.Length; ++i)
            {
                if (int.TryParse(inputs[i], out int num))
                    player2.Add(num);
            }

            Console.WriteLine($"Part 1: {Part1()}");
            Console.WriteLine($"Part 2: {Part2()}");
        }

        int Part1()
        {
            Queue<int> tempPlayer1 = new Queue<int>(player1);
            Queue<int> tempPlayer2 = new Queue<int>(player2);

            int turns = 0;
            while (tempPlayer1.Any() && tempPlayer2.Any())
            {
                ++turns;
                int player1Card = tempPlayer1.Dequeue();
                int player2Card = tempPlayer2.Dequeue();
                if (player1Card > player2Card)
                {
                    tempPlayer1.Enqueue(player1Card);
                    tempPlayer1.Enqueue(player2Card);
                }
                else
                {
                    tempPlayer2.Enqueue(player2Card);
                    tempPlayer2.Enqueue(player1Card);
                }
            }

            return tempPlayer1.Any() ? CalculateScore(tempPlayer1) : CalculateScore(tempPlayer2);
        }

        int Part2()
        {
            var (player1Win, player1Deck, player2Deck) = RecursiveCombat(new Queue<int>(player1), new Queue<int>(player2));
            return player1Win ? CalculateScore(player1Deck) : CalculateScore(player2Deck);
        }

        (bool player1Win, Queue<int> player1, Queue<int> player2) RecursiveCombat(Queue<int> tempPlayer1, Queue<int> tempPlayer2)
        {
            (List<List<int>> player1State, List<List<int>> player2State) playersState = (new List<List<int>>(), new List<List<int>>());
            bool player1Win = false;

            while (tempPlayer1.Any() && tempPlayer2.Any())
            {
                //check the order existed before
                if (CheckState(playersState, tempPlayer1, tempPlayer2))
                {
                    return (true, tempPlayer1, tempPlayer2);
                }

                // add new state
                playersState.player1State.Add(tempPlayer1.ToList());
                playersState.player2State.Add(tempPlayer2.ToList());

                int player1Card = tempPlayer1.Dequeue();
                int player2Card = tempPlayer2.Dequeue();

                if (tempPlayer1.Count >= player1Card && tempPlayer2.Count >= player2Card)
                {
                    player1Win = RecursiveCombat(new Queue<int>(tempPlayer1.Take(player1Card)), new Queue<int>(tempPlayer2.Take(player2Card))).player1Win;
                }
                else
                {
                    player1Win = player1Card > player2Card;
                }

                if (player1Win)
                {
                    tempPlayer1.Enqueue(player1Card);
                    tempPlayer1.Enqueue(player2Card);
                }
                else
                {
                    tempPlayer2.Enqueue(player2Card);
                    tempPlayer2.Enqueue(player1Card);
                }
            }

            return (player1Win, tempPlayer1, tempPlayer2);
        }

        private bool CheckState((List<List<int>> player1State, List<List<int>> player2State) playersState, Queue<int> currPlayer1State, Queue<int> currPlayer2State)
        {
            return playersState.player1State.Where(state => state.Count == currPlayer1State.Count).Any(state => state.SequenceEqual(currPlayer1State)) ||
                playersState.player2State.Where(state => state.Count == currPlayer2State.Count).Any(state => state.SequenceEqual(currPlayer2State));
        }


        private int CalculateScore(Queue<int> cardDeck)
        {
            int count = cardDeck.Count;
            return cardDeck.Select(x => x * count--).Sum();
        }
    }
}
