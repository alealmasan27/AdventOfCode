using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day08
{
    internal class Day08
    {
        private readonly List<Instruction> _instructions;

        private const int InitialAccumulatorValue = 0;
        private int _i;

        public Day08()
        {
            _instructions = ProcessReadData();
        }

        private static List<Instruction> ProcessReadData()
        {
            return ReadData().Select(line => line.Split(" ")).Select(splitLine => new Instruction {Operation = splitLine[0], Argument = Convert.ToInt32(splitLine[1])}).ToList();
        }

        private static IEnumerable<string> ReadData()
        {
            return File.ReadAllLines("Day08.txt").ToList();
        }

        public int ValueInAccumulator()
        {
            var listOfVisitedIndexes = new List<int>();
            var accumulatorValue = InitialAccumulatorValue;
            _i = 0;
            while (!listOfVisitedIndexes.Contains(_i) && _i < _instructions.Count)
            {
                switch (_instructions[_i].Operation)
                {
                    case "nop":
                        listOfVisitedIndexes.Add(_i);
                        _i++;
                        break;
                    case "acc":
                        accumulatorValue += _instructions[_i].Argument;
                        listOfVisitedIndexes.Add(_i);
                        _i++;
                        break;
                    case "jmp":
                        listOfVisitedIndexes.Add(_i);
                        _i += _instructions[_i].Argument;
                        break;
                }
            }

            return accumulatorValue;
        }

        public int AccumulatorAfterChange()
        {
            var j = 0;
            while (j < _instructions.Count)
            {
                if ((_instructions[j].Operation == "nop" || _instructions[j].Operation == "jmp") &&
                    _instructions[j].Argument != 0)
                {
                    _instructions[j].Operation = _instructions[j].Operation == "nop" ? "jmp" : "nop";
                    var acc = ValueInAccumulator();
                    if (_i == _instructions.Count)
                    {
                        return acc;
                    }

                    _instructions[j].Operation = _instructions[j].Operation == "nop" ? "jmp" : "nop";
                }

                j++;
            }
            throw new Exception("There is no possible value for the accumulator");
        }
    }
}
