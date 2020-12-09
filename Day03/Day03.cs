using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day03
{
    internal class Day03
    {
        private readonly List<string> _lines;

        public Day03()
        {
            _lines = ReadData();
        }

        public List<string> ReadData()
        {
            return File.ReadAllLines("Day03.txt").ToList();
        }

        public long CountTrees(int down, int right)
        {
            long charToRight = 0, count = 0, lineCount = _lines[0].Length;
            for (var i = 0; i < _lines.Count; i += down)
            {
                if (charToRight >= lineCount)
                    charToRight -= lineCount;
                if (_lines[i].ElementAt((int)charToRight) == '#')
                    count++;
                charToRight += right;
            }

            return count;
        }
    }
}
