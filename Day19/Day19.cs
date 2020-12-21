using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day19
{
    internal class Day19
    {
        private readonly Dictionary<int, (List<string>, List<string>)> _rules;
        private readonly List<string> _inputMessages;

        public Day19()
        {
            _rules = new Dictionary<int, (List<string>, List<string>)>();
            _inputMessages = new List<string>();
            ReadData();
        }

        private void ReadData()
        {
            var changeWhereToAdd = false;
            foreach (var line in File.ReadAllLines("Day19.txt"))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (changeWhereToAdd) _inputMessages.Add(line);
                    else
                    {
                        var splitLine = line.Split(':');
                        var commands = splitLine[1].Split('|');
                        _rules.Add(Convert.ToInt32(splitLine[0]),
                            commands.Length > 1
                                ? (commands[0].Trim(' ', '"').Split(" ").ToList(), commands[1].Trim(' ', '"').Split(" ").ToList())
                                : (commands[0].Trim(' ', '"').Split(" ").ToList(), new List<string>()));
                    }
                }
                else
                    changeWhereToAdd = true;
            }
        }

        public long CountValidMessages()
        {
            return _inputMessages.Count(msg => MatchesRule(msg, new Stack<string>(_rules[0].Item1.Clone().Reverse())));
        }

        public long CountValidMessages2()
        {
            _rules[8] = (new List<string> { "42" }, new List<string> { "42", "8" });
            _rules[11] = (new List<string> { "42", "31" }, new List<string> { "42", "11", "31" });
            
            return _inputMessages.Count(msg => MatchesRule(msg, new Stack<string>(_rules[0].Item1.Clone().Reverse())));
        }

        private bool MatchesRule(string message, Stack<string> stack)
        {
            if (message.Length < stack.Count) return false;
            if (message.Length == 0 || stack.Count == 0) return message.Length == 0 && stack.Count == 0;
            var currentRule = stack.Pop();
            if ((currentRule == "a" || currentRule == "b") && message[0] == currentRule[0]) return MatchesRule(message[1..], stack.Clone());
            if (!int.TryParse(currentRule, out var result)) return false;
            var rules = _rules[result];
            foreach (var rule in new List<List<string>> { rules.Item1, rules.Item2 })
                if (rule.Count > 0)
                {
                    var newStack = stack.Clone();
                    foreach (var x in rule.Clone().Reverse())
                        newStack.Push(x);

                    if (MatchesRule(message, newStack)) return true;
                }
            return false;
        }
    }
}
