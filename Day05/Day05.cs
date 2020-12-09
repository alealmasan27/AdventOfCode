using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day05
{
    internal class Day05
    {
        private readonly List<string> _spacePartitions;
        private List<long> _seatIds;
        private const int FirstRow = 0;
        private const int FirstColumn = 0;
        private const int LastRow = 127;
        private const int LastColumn = 7;

        public Day05()
        {
            _spacePartitions = ReadData();
            _seatIds = new List<long>();
        }

        private static List<string> ReadData()
        {
            return File.ReadAllLines("Day05.txt").ToList();
        }

        public long HighestSeatId()
        {
            long highestSeatId = 0;
            foreach (var spacePartition in _spacePartitions)
            {
                int lowerHalfRow = FirstRow, upperHalfRow = LastRow, lowerHalfColumn = FirstColumn, upperHalfColumn = LastColumn;
                foreach (var charInPartition in spacePartition)
                {
                    switch (charInPartition)
                    {
                        case 'F':
                            upperHalfRow = (upperHalfRow + lowerHalfRow + 1) / 2;
                            break;
                        case 'B':
                            lowerHalfRow = (upperHalfRow + lowerHalfRow + 1) / 2;
                            break;
                        case 'L':
                            upperHalfColumn = (lowerHalfColumn + upperHalfColumn + 1) / 2;
                            break;
                        case 'R':
                            lowerHalfColumn = (lowerHalfColumn + upperHalfColumn + 1) / 2;
                            break;
                    }
                }
                long seatId = lowerHalfRow * 8 + lowerHalfColumn;
                _seatIds.Add(seatId);
                if (seatId > highestSeatId)
                    highestSeatId = seatId;
            }
            return highestSeatId;
        }

        public long FindYourSeatId()
        {
            _seatIds.Sort();
            for(var i = 0; i < _seatIds.Count - 1; i++)
                if (_seatIds[i + 1] - _seatIds[i] != 1)
                    return _seatIds[i] + 1;
            throw new Exception("There is no such seat on the airplane");
        }
    }
}
