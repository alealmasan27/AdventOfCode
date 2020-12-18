using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day18
{
    internal class Day18
    {
        private readonly List<string> _input = File.ReadAllLines("Day18.txt").ToList();
        private readonly Dictionary<char, int> _importancePart1 = new Dictionary<char, int> { { '+', 1 }, { '*', 1 }, { '-', 1 }, { '/', 1 }, {'(', 0}, {')', 0} };
        private readonly Dictionary<char, int> _importancePart2 = new Dictionary<char, int> { { '+', 2 }, { '*', 1 }, { '-', 2 }, { '/', 1 }, { '(', 0 }, { ')', 0 } };

        public long Eval() =>  _input.Sum(x => SumEval(x, _importancePart1));

        public long EvalAdvanced() => _input.Sum(x => SumEval(x, _importancePart2));

        public long SumEval(string expr, Dictionary<char, int> operatorPrecedence)
        {
            var valueStack = new Stack<long>();
            var operationStack = new Stack<char>();
            var i = 0;
            while (i < expr.Length)
            {
                if (expr[i] != ' ')
                {
                    if (expr[i] == '(') operationStack.Push('(');
                    else if (expr[i].ToString().All(char.IsDigit))
                    {
                        var value = 0L;
                        while (i < expr.Length && expr[i].ToString().All(char.IsDigit))
                            value = value * 10 + Convert.ToInt64(expr[i++].ToString());
                        valueStack.Push(value);
                        i--;
                    }
                    else if (expr[i] == ')')
                    {
                        while (operationStack.Peek() != '(')
                            valueStack.Push(ApplyOperation(valueStack.Pop(), valueStack.Pop(), operationStack.Pop()));
                        operationStack.Pop();
                    }
                    else
                    {
                        while (operationStack.Count!= 0 && operatorPrecedence[operationStack.Peek()] >= operatorPrecedence[expr[i]])
                            valueStack.Push(ApplyOperation(valueStack.Pop(), valueStack.Pop(), operationStack.Pop()));
                        operationStack.Push(expr[i]);
                    }
                }
                i++;
            }
            while (operationStack.Count != 0)
                valueStack.Push(ApplyOperation(valueStack.Pop(), valueStack.Pop(), operationStack.Pop()));
            return valueStack.Peek();
        }

        private static long ApplyOperation(long value1, long value2, char pop)
        {
            return pop switch
            {
                '+' => value1 + value2,
                '-' => value1 - value2,
                '*' => value1 * value2,
                '/' => value2 / value1,
                _ => throw new Exception("Invalid Format")
            };
        }
    }
}