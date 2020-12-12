using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day10
{
    internal class Day10
    {
        private readonly List<int> _outputJoltage;

        public Day10()
        {
            _outputJoltage = ReadData();
            _outputJoltage.Sort();
        }

        public List<int> ReadData()
        {
            return File.ReadAllLines("Day010.txt").Select(x => Convert.ToInt32(x)).ToList();
        }

        public long Multiply1JoltWith3Jolt()
        {
            long threeJoltsDifference = 1, oneJoltsDifference = 1;
            for (var i = 0; i < _outputJoltage.Count - 1; i++)
            {
                switch (_outputJoltage[i + 1] - _outputJoltage[i])
                {
                    case 1:
                        oneJoltsDifference++;
                        break;
                    case 3:
                        threeJoltsDifference++;
                        break;
                }
            }
            return oneJoltsDifference * threeJoltsDifference;
        }


        public long CountNumberOfArrangements()
        {
            _outputJoltage.Insert(0, 0);
            var arrangements = Enumerable.Repeat(0L, _outputJoltage.Count).ToList();
            arrangements[0] = 1;
            for (var i = 1; i < _outputJoltage.Count; i++)
            {
                if (i - 3 >= 0 && _outputJoltage[i] - _outputJoltage[i - 3] <= 3) arrangements[i] += arrangements[i - 3];
                if (i - 2 >= 0 && _outputJoltage[i] - _outputJoltage[i - 2] <= 3) arrangements[i] += arrangements[i - 2];
                if (i - 1 >= 0 && _outputJoltage[i] - _outputJoltage[i - 1] <= 3) arrangements[i] += arrangements[i - 1];
            }
            return arrangements.Last();
        }

    }
}
