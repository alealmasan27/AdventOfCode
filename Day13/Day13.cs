using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Day13
{
    internal class Day13
    {
        private readonly List<int> _busIds;
        private readonly List<string> _allBuses;
        private int _departureTime;

        public Day13()
        {
            _busIds = new List<int>();
            _allBuses = new List<string>();
            ReadData();
        }

        private void ReadData()
        {
            var lines = File.ReadAllLines("Day13.txt");
            _departureTime = Convert.ToInt32(lines[0]);
            _allBuses.AddRange(lines[1].Split(","));
            _busIds.AddRange(lines[1].Split(",").Where(x => x != "x").Select(int.Parse));
        }

        public long MultiplyBusIdWithMinutesToWait()
        {
            var closestTimeStampToDepartureList = _busIds.Select(x => ((_departureTime + x - 1) / x) * x).ToList();
            var index = closestTimeStampToDepartureList.IndexOf(closestTimeStampToDepartureList.Min());
            return _busIds[index] * (closestTimeStampToDepartureList[index] - _departureTime);
        }

        public BigInteger EarliestTimestamp()
        {
            var remainder = _busIds.Select(x => x - _allBuses.IndexOf(x.ToString())).ToList();
            remainder[0] = 0;
            return SolveChineseRemainderTheorem(_busIds.Select(x => new BigInteger(x)).ToList(), remainder.Select(x => new BigInteger(x)).ToList());
        }

        public static BigInteger SolveChineseRemainderTheorem(List<BigInteger> n, List<BigInteger> a)
        {
            var prod = n.Aggregate(BigInteger.One, (i, j) => i * j);
            BigInteger sm = 0;
            for (var i = 0; i < n.Count; i++)
            {
                var p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static BigInteger ModularMultiplicativeInverse(BigInteger a, BigInteger mod)
        {
            var b = a % mod;
            for (var x = 1; x < mod; x++)
            {
                if (b * x % mod == 1)
                {
                    return x;
                }
            }
            return BigInteger.One;
        }
    }
}
