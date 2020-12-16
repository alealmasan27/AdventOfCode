using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day16
{
    internal class Day16
    {
        private readonly List<(int, int)> _inputFilters;
        private readonly Dictionary<int, List<int>> _tickets;
        private readonly List<int> _indexInvalidTickets;

        public Day16()
        {
            _inputFilters = new List<(int, int)>();
            _tickets = new Dictionary<int, List<int>>();
            _indexInvalidTickets = new List<int>();
            ReadData();
        }

        private void ReadData()
        {
            var readTickets = false;
            var index = 0;
            foreach (var line in File.ReadAllLines("Day16.txt"))
            {
                if (string.IsNullOrEmpty(line)) readTickets = true;
                else
                {
                    if (!readTickets)
                    {
                        var lineSplit = line.Split().Skip(1);
                        foreach (var lineElement in lineSplit)
                        {
                            var takeTheTwoNumbers = lineElement.Split('-');
                            if (takeTheTwoNumbers.Length == 2)
                                _inputFilters.Add((Convert.ToInt32(takeTheTwoNumbers[0]),
                                    Convert.ToInt32(takeTheTwoNumbers[1])));
                        }
                    }
                    else
                    {
                        var lineSplit = line.Split(',');
                        if (lineSplit.Length <= 1) continue;
                        _tickets.Add(index, lineSplit.Select(int.Parse).ToList());
                        index++;
                    }
                }
            }
        }

        public long TicketScanningErrorRate()
        {
            var errorRate = 0L;
            for (var i = 1; i < _tickets.Values.Count; i++)
            {
                foreach (var ticketField in _tickets[i].Where(ticketField => !_inputFilters.Any(x => x.Item1 <= ticketField && ticketField <= x.Item2)))
                {
                    errorRate += ticketField;
                    _indexInvalidTickets.Add(i);
                }
            }
            return errorRate;
        }

        public long MultiplyDepartureValues()
        {
            foreach (var id in _indexInvalidTickets) _tickets.Remove(id);
            var ticketFieldsToPossibleColumns = new Dictionary<int, List<int>>();
            for (var i = 0; i < _inputFilters.Count; i += 2)
            {
                var indexList = FindIndexOfPossibleColumns(_inputFilters[i], _inputFilters[i + 1], _tickets);
                ticketFieldsToPossibleColumns.Add(i / 2, indexList);
            }
            var fieldToColumn = GetFieldToColumn(ticketFieldsToPossibleColumns, new Dictionary<int, int>());
            return MultiplyFirst6TicketFields(fieldToColumn);
        }

        private static Dictionary<int, int> GetFieldToColumn(Dictionary<int, List<int>> ticketFieldsToPossibleColumns, Dictionary<int, int> fieldToColumn)
        {
            while (ticketFieldsToPossibleColumns.Any(x => x.Value.Count > 0))
            {
                var singleElement = ticketFieldsToPossibleColumns.FirstOrDefault(x => x.Value.Count == 1);
                fieldToColumn.Add(singleElement.Key, singleElement.Value[0]);

                ticketFieldsToPossibleColumns = ticketFieldsToPossibleColumns.Select(y => new { Index = y.Key, ListOfPossibleColumns = y.Value.Where(t => t != singleElement.Value[0]).ToList() }).ToDictionary(t => t.Index, t => t.ListOfPossibleColumns);
            }
            return fieldToColumn;
        }

        private long MultiplyFirst6TicketFields(IReadOnlyDictionary<int, int> fieldToColumn)
        {
            var multiply = 1L;
            for (var i = 0; i < 6; i++) multiply *= _tickets[0][fieldToColumn[i]];
            return multiply;
        }

        private static List<int> FindIndexOfPossibleColumns((int, int) firstRange, (int, int) secondRange, IReadOnlyDictionary<int, List<int>> tickets)
        {
            var indexList = new List<int>();
            for (var i = 0; i < tickets[0].Count; i++)
            {
                var listOfElementsOnIndex = tickets.Skip(1).Select(x => x.Value[i]).ToList();
                if (listOfElementsOnIndex.All(x => firstRange.Item1 <= x && x <= firstRange.Item2 || secondRange.Item1 <= x && x <= secondRange.Item2)) indexList.Add(i);
            }
            return indexList;
        }
    }
}
