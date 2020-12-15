using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day15
{
    internal class Day15
    {
        private List<long> _inputs;
        private Dictionary<long, long> _lastAppearancePosition;

        private void Initializations()
        {
            _inputs = new List<long>();
            _lastAppearancePosition = new Dictionary<long, long>();
            ReadData();
            for (var i = 0; i < _inputs.Count - 1; i++)
                _lastAppearancePosition.Add(_inputs[i], i + 1);
        }

        private void ReadData()
        {
            _inputs.AddRange(File.ReadAllLines("Day015.txt")[0].Split(',').Select(long.Parse));
        }


        public long NthNumberSpoken(long nthNumberSpokenIndex)
        {
            Initializations();
            for (var i = _inputs.Count; i < nthNumberSpokenIndex; i++)
            {
                if (_lastAppearancePosition.TryGetValue(_inputs[i - 1], out var index))
                {
                    _lastAppearancePosition[_inputs[i - 1]] = i;
                    _inputs.Add(i - index);
                }
                else
                {
                    _lastAppearancePosition.Add(_inputs[i - 1], i);
                    _inputs.Add(0);
                }
            }

            return _inputs[^1];
        }
    }
}
