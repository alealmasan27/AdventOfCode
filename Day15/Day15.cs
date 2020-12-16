using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day15
{
    internal class Day15
    {
        private readonly List<long> _inputs;
        private Dictionary<long, long> _lastAppearancePosition;

        public Day15()
        {
            _inputs = new List<long>(File.ReadAllLines("Day15.txt")[0].Split(',').Select(long.Parse));
        }

        private void DictionaryInitialization()
        {
            _lastAppearancePosition = new Dictionary<long, long>();
            for (var i = 0; i < _inputs.Count - 1; i++) //without the last element of the list
                _lastAppearancePosition.Add(_inputs[i], i + 1);
        }

        public long NthNumberSpoken(long nthNumberSpokenIndex)
        {
            DictionaryInitialization();
            var lastNumberSpoken = _inputs[^1];
            for (var i = _inputs.Count; i < nthNumberSpokenIndex; i++)
            {
                if (_lastAppearancePosition.TryGetValue(lastNumberSpoken, out var index))
                {
                    _lastAppearancePosition[lastNumberSpoken] = i;
                    lastNumberSpoken = i - index;
                }
                else
                {
                    _lastAppearancePosition.Add(lastNumberSpoken, i);
                    lastNumberSpoken = 0;
                }
            }
            return lastNumberSpoken;
        }
    }
}
