using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day22
{
    class Day22
    {
        private readonly Queue<int> _player1;

        private readonly Queue<int> _player2;

        public Day22()
        {
            _player1 = new Queue<int>();
            _player2 = new Queue<int>();
            ReadData();
        }

        private void ReadData()
        {
            var isSecondList = true;
            foreach (var line in File.ReadAllLines("Day22.txt"))
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.IndexOf("Player", StringComparison.Ordinal) != -1)
                {
                    isSecondList = !isSecondList;
                    continue;
                }
                if (isSecondList) _player2.Enqueue(Convert.ToInt32(line));
                else _player1.Enqueue(Convert.ToInt32(line));
            }
        }

        public long CombatWinningScore()
        {
            while (_player1.Count > 0 && _player2.Count > 0)
            {
                var (playerOneCard, playerTwoCard) = (_player1.Dequeue(), _player2.Dequeue());
                if (playerOneCard > playerTwoCard)
                {
                    _player1.Enqueue(playerOneCard);
                    _player1.Enqueue(playerTwoCard);
                }
                else
                {
                    _player2.Enqueue(playerTwoCard);
                    _player2.Enqueue(playerOneCard);
                }
            }
            return _player1.Count != 0 ? FindTotalScore(_player1) : FindTotalScore(_player2);
        }

        public long RecursiveCombatWinningScore()
        {
            ReadData();
            var gameResult = PlayRecursiveCombat(_player1, _player2, new HashSet<int>());
            return FindTotalScore(gameResult.Item2);
        }
        

        private static (int, Queue<int>) PlayRecursiveCombat(Queue<int> player1, Queue<int> player2, HashSet<int> seen)
        {
            while (player1.Count > 0 && player2.Count > 0)
            {
                var state = (player1.ToList(), player2.ToList()).GetHashCode();
                if (seen.Contains(state)) return (1, player1);

                seen.Add(state);
                var (player1Card, player2Card) = (player1.Dequeue(), player2.Dequeue());
                int winner;
                if (player1.Count >= player1Card && player2.Count >= player2Card)
                {
                    var result = PlayRecursiveCombat(new Queue<int>(player1.Take(player1Card)), new Queue<int>(player2.Take(player2Card)),
                        new HashSet<int>());
                    winner = result.Item1;
                }
                else
                    winner = player1Card > player2Card ? 1 : 2;

                if (winner == 1)
                {
                    player1.Enqueue(player1Card);
                    player1.Enqueue(player2Card);
                }
                else
                {
                    player2.Enqueue(player2Card);
                    player2.Enqueue(player1Card);
                }
            }

            return player1.Count > 0 ? (1, player1) : (2, player2);
        }

        private static long FindTotalScore(Queue<int> queue)
        {
            var sum = 0;
            while (queue.Count != 0) sum += queue.Count * queue.Dequeue();
            return sum;
        }
    }
}
