using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day20
{
    internal class Day20
    {
        private static List<Tile> _tiles;
        private static Dictionary<string, List<Tile>> _pairs;
        private static Tile[][] _board;
        private static readonly int _numberOfOrientations = 8;

        public Day20()
        {
            _tiles = ReadData();
            AssembleBoard();
        }

        private static List<Tile> ReadData()
        {
            var lines = File.ReadAllLines("Day20.txt").Select(line => line.Trim()).ToList();
            var input = lines.Split(lines.FindIndex(string.IsNullOrEmpty));
            return input.Select(tile => new Tile(long.Parse(tile[0].Split(" ")[1].Replace(":", "")), tile.Skip(1).ToArray())).ToList();
        }

        public long MultiplyBorderTileNumbers()
        {
            return _board.First().First().Id * _board.First().Last().Id * _board.Last().First().Id * _board.Last().Last().Id;
        }

        public long CountSharpsWhichAreNotPartOfSeaMonsters()
        {
            var image = UnifyTilesWithoutFirstAndLastChar(_board);
            var seaMonster = new List<string> {"                  # ", "#    ##    ##    ###", " #  #  #  #  #  #   "};
            int monsterCount;
            while ((monsterCount = CountSeaMonsters(image, seaMonster)) == 0)
                image.ChangeOrientation();
            var hashCountInImage = image.ToString().Count(ch => ch == '#');
            var hashCountInMonster = string.Join("\n", seaMonster).Count(ch => ch == '#');
            return hashCountInImage - monsterCount * hashCountInMonster;
        }

        private static bool IsEdge(string pattern) => _pairs[pattern].Count == 1;

        private static Tile GetNeighbour(Tile tile, string pattern) => _pairs[pattern].SingleOrDefault(other => other != tile);

        private static Tile PutTileInPlace(Tile above, Tile left)
        {
            if (above == null && left == null)
            {
                foreach (var tile in _tiles)
                {
                    for (var i = 0; i < 8; i++)
                    {
                        if (IsEdge(tile.Top()) && IsEdge(tile.Left())) return tile;
                        tile.ChangeOrientation();
                    }
                }
            }
            else
            {
                var tile = above != null ? GetNeighbour(above, above.Bottom()) : GetNeighbour(left, left.Right());
                while (true)
                {
                    var topMatch = above == null ? IsEdge(tile.Top()) : tile.Top() == above.Bottom();
                    var leftMatch = left == null ? IsEdge(tile.Left()) : tile.Left() == left.Right();
                    if (topMatch && leftMatch) return tile;
                    tile.ChangeOrientation();
                }
            }
            throw new Exception("There is no possible match");
        }

        private static void AssembleBoard()
        {
            _pairs = new Dictionary<string, List<Tile>>();
            foreach (var tile in _tiles)
            {
                for (var i = 0; i < _numberOfOrientations; i++)
                {
                    var pattern = tile.Top();
                    if (!_pairs.ContainsKey(pattern)) _pairs[pattern] = new List<Tile>();

                    _pairs[pattern].Add(tile);
                    tile.ChangeOrientation();
                }
            }
            
            var size = (int) Math.Sqrt(_tiles.Count);
            _board = new Tile[size][];
            for (var row = 0; row < size; row++)
            {
                _board[row] = new Tile[size];
                for (var col = 0; col < size; col++)
                {
                    var above = row == 0 ? null : _board[row - 1][col];
                    var left = col == 0 ? null : _board[row][col - 1];
                    _board[row][col] = PutTileInPlace(above, left);
                }
            }
        }

        private static Tile UnifyTilesWithoutFirstAndLastChar(IReadOnlyList<Tile[]> tiles)
        {
            var image = new List<string>();
            var tileSize = tiles[0][0].Size;
            foreach (var tile in tiles)
            {
                for (var i = 1; i < tileSize - 1; i++)
                {
                    var columnPattern = "";
                    for (var col = 0; col < tiles.Count; col++) columnPattern = $"{columnPattern}{tile[col].Row(i)[1..(tileSize - 1)]}";
                    image.Add(columnPattern);
                }
            }
            return new Tile(0, image.ToArray());
        }
        
        private static int CountSeaMonsters(Tile image, IReadOnlyList<string> pattern)
        {
            var count = 0;
            for (var row = 0; row < image.Size - pattern.Count; row++)
                for (var col = 0; col < image.Size - pattern[0].Length; col++)
                    if (MatchSeaMonster(pattern, image, row, col)) count++;
            return count;
        }

        private static bool MatchSeaMonster(IReadOnlyList<string> pattern, Tile image, int row, int col)
        {
            for (var j = 0; j < pattern[0].Length; j++)
                if (pattern.Where((t, i) => t[j] == '#' && image[row + i, col + j] != '#').Any())
                    return false;
            return true;
        }
    }
}