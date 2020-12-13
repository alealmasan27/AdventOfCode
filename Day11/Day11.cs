using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day11
{
    internal class Day11
    {
        private SeatStatus[,] _seatSituation;
        private readonly List<int> _lineIndexes = new List<int> { -1, 0, 1, 1, 1, 0, -1, -1 };
        private readonly List<int> _columnIndexes = new List<int> { 1, 1, 1, 0, -1, -1, -1, 0 };

        public Day11()
        {
            ReadData();
        }

        private void ReadData()
        {
            var lines = File.ReadAllLines("Day11.txt").ToList();
            _seatSituation = new SeatStatus[lines.Count, lines[0].Length];
            for (var i = 0; i < lines.Count; i++)
            {
                for (var indexChar = 0; indexChar < lines[i].Length; indexChar++)
                {
                    _seatSituation[i, indexChar] = lines[i][indexChar] switch
                    {
                        '#' => SeatStatus.Occupied,
                        '.' => SeatStatus.Floor,
                        'L' => SeatStatus.Empty,
                        _ => throw new Exception()
                    };
                }
            }
        }

        public long NumberOfOccupiedSeats(Func<SeatStatus[,], int, int, int> countFunction, int numberOfOccupiedSeats)
        {
            var seatsAux = (SeatStatus[,]) _seatSituation.Clone();
            bool changed;
            SeatStatus[,] seats;
            do
            {
                seats = (SeatStatus[,]) seatsAux.Clone();
                changed = false;
                for (var line = 0; line < seats.GetLength(0); line++)
                {
                    for (var column = 0; column < seats.GetLength(1); column++)
                    {
                        var count = countFunction(seats, line, column);
                        switch (seats[line, column])
                        {
                            case SeatStatus.Empty:
                                if (count == 0)
                                {
                                    seatsAux[line,column] = SeatStatus.Occupied;
                                    changed = true;
                                }
                                break;
                            case SeatStatus.Occupied:
                                if (count >= numberOfOccupiedSeats)
                                {
                                    seatsAux[line, column] = SeatStatus.Empty;
                                    changed = true;
                                }
                                break;
                            case SeatStatus.Floor:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            } while (changed);
            return CountSeatsOccupied(seats);
        }

        private static long CountSeatsOccupied(SeatStatus[,] seats)
        {
            var count = 0;
            for(var i = 0;i<seats.GetLength(0);i++)
                for(var j = 0 ;j<seats.GetLength(1);j++)
                    if (seats[i, j] == SeatStatus.Occupied)
                        count++;
            return count;
        }

        public int CheckAdjacentSeats(SeatStatus[,] seats, int line, int column)
        {
            var count = 0;
            for (var i = 0; i < _lineIndexes.Count; i++)
            {
                var newLine = line + _lineIndexes[i];
                var newColumn = column + _columnIndexes[i];
                if (newLine >= 0 && newColumn >= 0 && newLine < seats.GetLength(0) && newColumn < seats.GetLength(1) && seats[newLine, newColumn] == SeatStatus.Occupied)
                {
                    count += 1;
                }
            }

            return count;
        }

        public int CheckAdjacentSeatsUntil(SeatStatus[,] seats, int line, int column)
        {
            var count = 0;
            for (var i = 0; i < _lineIndexes.Count; i++)
            {
                var newLine = line;
                var newColumn = column;
                while (true)
                {
                    newLine += _lineIndexes[i];
                    newColumn += _columnIndexes[i];

                    if (newLine < 0 || newColumn < 0 || newLine >= seats.GetLength(0) || newColumn >= seats.GetLength(1))
                        break;

                    if (seats[newLine, newColumn] == SeatStatus.Occupied)
                    {
                        count++;
                        break;
                    }

                    if (seats[newLine, newColumn] == SeatStatus.Empty)
                    {
                        break;
                    }
                    
                }
                
            }
            return count;
        }
    }
}
