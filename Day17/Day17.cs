using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day17
{
    public class Day17
    {
        private readonly char[][] _values = File.ReadAllLines("Day17.txt").Select(line => line.ToCharArray()).ToArray();
        private readonly List<int> _directions = new List<int> {-1, 0, 1};

        private IEnumerable<Tuple<int, int, int>> Neighbours(int x, int y, int z)
        {
            var neighbours = new HashSet<Tuple<int, int, int>>();
            foreach (var dx in _directions)
                foreach (var dy in _directions)
                    foreach (var dz in _directions.Where(dz => dx != 0 || dy != 0 || dz != 0))
                        neighbours.Add(Tuple.Create(x + dx, y + dy, z + dz));
            return neighbours;
        }

        private IEnumerable<Tuple<int, int, int, int>> Neighbours(int x, int y, int z, int w)
        {
            var neighbours = new HashSet<Tuple<int, int, int, int>>();
            foreach (var dx in _directions)
                foreach (var dy in _directions)
                    foreach (var dz in _directions)
                        foreach (var dw in _directions.Where(dw => dx != 0 || dy != 0 || dz != 0 || dw != 0))
                            neighbours.Add(Tuple.Create(x + dx, y + dy, z + dz, w + dw));
            return neighbours;
        }

        private HashSet<T> Neighbours<T>(T element)
        {
            if (element is Tuple<int, int, int> value1) 
                return Neighbours(value1.Item1, value1.Item2, value1.Item3) as HashSet<T>;
            var value = element as Tuple<int, int, int, int>;
            return Neighbours(value.Item1, value.Item2, value.Item3, value.Item4) as HashSet<T>;
        }

        private int CountNeighbours<T>(HashSet<T> cubes, T element)
        {
            return Neighbours(element).Count(cubes.Contains);
        }

        private HashSet<T> Step<T>(HashSet<T> cubes)
        {
            var newCubes = new HashSet<T>();
            foreach (var p in cubes)
            {
                foreach (var neighbor in Neighbours(p)
                    .Where(neighbor => !cubes.Contains(neighbor) && CountNeighbours(cubes, neighbor) == 3))
                    newCubes.Add(neighbor);

                if (new List<int> {2, 3}.Contains(CountNeighbours(cubes, p)))
                    newCubes.Add(p);
            }
            return newCubes;
        }

        private HashSet<T> GetCubes<T>(int dim) where T: class
        {
            var cubes = new HashSet<T>();
            for (var i = 0; i < _values.Length; i++)
            for (var j = 0; j < _values[0].Length; j++)
                    if (_values[i][j] == '#')
                        cubes.Add(dim == 3 ? Tuple.Create(i, j, 0) as T : Tuple.Create(i, j, 0, 0) as T);
            return cubes;
        }

        public int CountActiveCubes<T>(int dim) where T: class
        {
            var cubes = GetCubes<T>(dim);
            for (var i = 0; i < 6; i++)
                cubes = Step(cubes);
            return cubes.Count;
        }
    }
}