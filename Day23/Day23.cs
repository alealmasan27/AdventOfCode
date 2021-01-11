using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day23
{
    internal class Day23
    {
        private readonly List<int> _cupLabels;

        public Day23()
        {
            _cupLabels = File.ReadAllText("Day23.txt").Select(x => Convert.ToInt32(x.ToString())).ToList();
        }

        public string CupLabelsAfter1() => string.Join("", CrabCupsSolver(_cupLabels, 9, 100).Take(8));

        public long MultiplyTheTwoLabelsAfter1() => CrabCupsSolver(_cupLabels, 1000000, 10000000).Take(2).Aggregate((x, y) => x * y);

        private static IEnumerable<long> CrabCupsSolver(IReadOnlyList<int> cupLabels, int maxLabel, int moves)
        {
            var next = Enumerable.Range(1, maxLabel + 1).ToList();
            for (var i = 0; i < cupLabels.Count; i++)
                next[cupLabels[i]] = cupLabels[(i + 1) % cupLabels.Count];
            if (maxLabel > cupLabels.Count)
            {
                next[maxLabel] = next[cupLabels.Last()];
                next[cupLabels.Last()] = cupLabels.Count + 1;
            }
            var current = cupLabels.First();
            for (var i = 0; i < moves; i++)
            {
                var (pickUp1, pickUp2, pickUp3) = (next[current], next[next[current]], next[next[next[current]]]);
                next[current] = next[pickUp3];
                
                var destination = current;
                do destination = destination == 1 ? maxLabel : --destination;
                while (destination == pickUp1 || destination == pickUp2 || destination == pickUp3);

                next[pickUp3] = next[destination];
                next[destination] = pickUp1;
                current = next[current];
            }
            var cup = next[1];
            while (true)
            {
                yield return cup;
                cup = next[cup];
            }
        }
    }
}