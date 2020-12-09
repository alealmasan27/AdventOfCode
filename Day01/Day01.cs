using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day01
{
    internal class Day01
    {
        private static List<int> ReadData()
        {
            return File.ReadAllLines("Day01.txt").Select(x => Convert.ToInt32(x)).ToList();
        }

        public Tuple<int, int> TwoNumbersThatSumTo2020()
        {
            var numbers = ReadData();
            foreach (var number in numbers)
            {
                var sum2020 = numbers.FirstOrDefault(x => x + number == 2020);
                if (sum2020 != (int?) 0)
                    return new Tuple<int, int>(number, sum2020);
            }
            throw new Exception("There aren't any 2 numbers which have the sum 2020");
        }

        public Tuple<int, int, int> ThreeNumbersThatSumTo2020()
        {
            var numbers = ReadData();
            for (var i = 0; i < numbers.Count; i++)
            for (var j = i + 1; j < numbers.Count; j++)
            for (var k = j + 1; k < numbers.Count; k++)
                if (numbers[i] + numbers[j] + numbers[k] == 2020)
                    return new Tuple<int, int, int>(numbers[i], numbers[j], numbers[k]);
            throw new Exception("There aren't any 3 numbers which have the sum 2020");
        }
    }
}
