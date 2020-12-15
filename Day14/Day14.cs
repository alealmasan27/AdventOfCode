using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day14
{
    internal class Day14
    {
        private readonly List<BitmaskInput> _bitmaskInputs;
        private Dictionary<long, long> _valuesInMemory;

        public Day14()
        {
            _bitmaskInputs = new List<BitmaskInput>();
            _valuesInMemory = new Dictionary<long, long>();
            ReadData();
        }

        private void ReadData()
        {
            var memList = new List<MemoryAddressWithValue>();
            foreach (var line in File.ReadAllLines("Day14.txt"))
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.IndexOf("mask", StringComparison.Ordinal) != -1)
                {
                    if (_bitmaskInputs.Count != 0)
                        _bitmaskInputs[^1].MemoryAddressWithValues = memList;
                    memList = new List<MemoryAddressWithValue>();
                    var bitmaskIndex = line.IndexOf('=') + 1;
                    if (bitmaskIndex >= 0)
                    {
                        _bitmaskInputs.Add(new BitmaskInput {Bitmask = line[bitmaskIndex..].TrimStart()});
                    }
                }
                else
                {
                    var startPos = line.IndexOf('[') + 1;
                    var endPos = line.IndexOf(']');
                    var valueStartPos = line.IndexOf('=') + 1;
                    memList.Add(new MemoryAddressWithValue
                    {
                        MemoryAddress = Convert.ToInt64(line.Substring(startPos, endPos - startPos)),
                        Value = Convert.ToInt64(line[valueStartPos..].TrimStart())
                    });
                }
            }
            _bitmaskInputs[^1].MemoryAddressWithValues = memList;
        }

        public long SumValuesInMemory()
        {
            foreach (var bitmaskInput in _bitmaskInputs)
            {
                foreach (var memAddresses in bitmaskInput.MemoryAddressWithValues)
                {
                    var value = Convert.ToString(memAddresses.Value, 2);
                    value = value.PadLeft(36, '0');
                    var result = "";
                    for (var i = 0; i < 36; i++)
                    {
                        result = bitmaskInput.Bitmask[i] == 'X' ? $"{result}{value[i]}" : $"{result}{bitmaskInput.Bitmask[i]}";
                    }

                    if (_valuesInMemory.ContainsKey(memAddresses.MemoryAddress))
                        _valuesInMemory[memAddresses.MemoryAddress] = Convert.ToInt64(result, 2);
                    else
                    {
                        _valuesInMemory.Add(memAddresses.MemoryAddress, Convert.ToInt64(result, 2));
                    }
                }
            }

            return _valuesInMemory.Sum(x => x.Value);
        }

        public long SumAllValuesInMemory()
        {
            _valuesInMemory = new Dictionary<long, long>();
            foreach (var bitmaskInput in _bitmaskInputs)
            {
                foreach (var memAddresses in bitmaskInput.MemoryAddressWithValues)
                {
                    var value = Convert.ToString(memAddresses.MemoryAddress, 2);
                    value = value.PadLeft(bitmaskInput.Bitmask.Length, '0');
                    var result = "";
                    for (var i = 0; i < 36; i++)
                    {
                        result = bitmaskInput.Bitmask[i] == 'X' ? $"{result}X" :
                            bitmaskInput.Bitmask[i] == '1' ? $"{result}1" : $"{result}{value[i]}";
                    }

                    GenerateAllPossibleResults(result, result.Length - 1, memAddresses.Value);
                }
            }

            return _valuesInMemory.Sum(x => x.Value);
        }

        private void GenerateAllPossibleResults(string result, int pos, long value)
        {
            if (pos == -1)
            {
                var number = Convert.ToInt64(result, 2);
                _valuesInMemory[number] = value;
            }
            else
            {
                if (result[pos] == 'X')
                {
                    for (var i = 0; i <= 1; i++)
                    {
                        result = $"{result[..pos]}{i}{result[(pos + 1)..]}";
                        GenerateAllPossibleResults(result, pos - 1, value);
                        result = $"{result[..pos]}X{result[(pos + 1)..]}";
                    }
                }
                else
                {
                    GenerateAllPossibleResults(result, pos - 1, value);
                }
                
            }
        }
    }
}
