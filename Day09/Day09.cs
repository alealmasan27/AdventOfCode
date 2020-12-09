using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day09
{
    internal class Day09
    {
        private readonly List<long> _data;
        private readonly int _preambleNo = 25;
        private readonly List<long> _preambleNumbers;
        private readonly List<long> _allReadNumbers;

        public Day09()
        {
            _preambleNumbers = new List<long>();
            _allReadNumbers = new List<long>();
            _data = ProcessData();
        }

        private static List<string> ReadData()
        {
            return File.ReadAllLines("Day09.txt").ToList();
        }

        private List<long> ProcessData()
        {
            var lines = ReadData();
            var longNumbers = new List<long>();
            for (var i = 0; i < lines.Count; i++)
            {
                var nr = Convert.ToInt64(lines[i]);
                if (i < _preambleNo)
                {
                    _preambleNumbers.Add(nr);
                }
                else
                {
                    longNumbers.Add(nr);
                }
                _allReadNumbers.Add(nr);
            }

            return longNumbers;
        }

        private bool CheckIfSumPossible(long nr)
        {
            for (var i = 0; i < _preambleNumbers.Count - 1; i++)
            {
                for (var j = i + 1; j < _preambleNumbers.Count; j++)
                {
                    if (_preambleNumbers[i] + _preambleNumbers[j] == nr)
                        return true;
                }
            }

            return false;
        }

        public long OddNumberFromSequence()
        {
            for (var i = 0; i < _data.Count;i++)
            {
                if (CheckIfSumPossible(_data[i]))
                {
                    _preambleNumbers[i % _preambleNo] = _data[i];
                }
                else
                {
                    return _data[i];
                }
            }
            throw new Exception("There wasn't any number which wasn't made by 2 numbers");
        }

        public long SumOfSmallestAndLargestNumbers(long nr)
        {
            var initialPos = 0;
            while (initialPos < _allReadNumbers.Count)
            {
                long sum = 0;
                var listOfNumbersThatSumNr = new List<long>();
                var i = initialPos;
                while (sum <= nr && i < _allReadNumbers.Count)
                {
                    if (sum == nr && listOfNumbersThatSumNr.Count > 1)
                    {
                        listOfNumbersThatSumNr.Sort();
                        return listOfNumbersThatSumNr[0] + listOfNumbersThatSumNr[^1];
                    }
                    listOfNumbersThatSumNr.Add(_allReadNumbers[i]);
                    sum += _allReadNumbers[i];
                    i++;
                }
                initialPos++;
            }
            throw new Exception("There are no such numbers");
        }
    }
}
