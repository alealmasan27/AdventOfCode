using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day07
{
    internal class Day07
    {
        private static Dictionary<string, List<(int, string)>> _luggageRules;
        private const string InitialBagColor = "shiny gold";

        public Day07()
        {
            _luggageRules = ProcessData();
        }

        private static IEnumerable<string> ReadData()
        {
            return File.ReadAllLines("Day07.txt").ToList();
        }

        private Dictionary<string, List<(int, string)>> ProcessData()
        {
            var luggageRules = new Dictionary<string, List<(int, string)>>();
            foreach (var line in ReadData())
            {
                var values = line.Split(new [] {"contain"}, StringSplitOptions.None);
                var mainColor = values[0].Replace("bags", "").Trim();
                var content = values[1].Contains("no other bags")
                    ? new List<(int, string)>()
                    : values[1][0..^1].Split(',').Select(value =>
                    {
                        var x = value.Trim().Split(' ');

                        return (int.Parse(x[0]), $"{x[1]} {x[2]}");
                    }).ToList();
                luggageRules[mainColor] = content;
            }

            return luggageRules;
        }

        private static bool CountRec(string color)
        {
            return _luggageRules[color].Exists(x => x.Item2 == InitialBagColor || CountRec(x.Item2));
        }

        public int CountColorBags()
        {
            //var colors = new HashSet<string>{InitialBagColor};

            //for (var i = 0; i < colors.Count; i++)
            //{
            //    var valuesThatContainKey = _luggageRules
            //        .Where(x => x.Value.Any(y => y.Item2 != null && y.Item2.Contains(colors.ElementAt(i)))).ToList();
            //    foreach (var value in valuesThatContainKey)
            //    {
            //        colors.Add(value.Key);
            //    }
            //}

            //return colors.Count - 1;
            return _luggageRules.Count(x => CountRec(x.Key));
        }

        private static long Count(IReadOnlyList<(int, string)> values)
        {
            if (values.Count > 0)
            {
                return values[0].Item1 + values[0].Item1 * Count(_luggageRules[values[0].Item2]);
            }
            return 0;
        }

        public int CountCorrect(string color)
        {
            return _luggageRules[color].Sum(content => content.Item1 + content.Item1 * CountCorrect(content.Item2));
        }

        public long CountInsideColorBags()
        {
            return CountCorrect(InitialBagColor);
        }
    }
}
