using System;

namespace AdventOfCode
{
    public class Program
    {
        private static void Main()
        {
            var day1 = new Day01.Day01();
            var (nr1, nr2) = day1.TwoNumbersThatSumTo2020();
            Console.WriteLine($"==Day01==\nThe multiplication of two numbers gives: {nr1 * nr2}");

            var threeNumbersThatSum2020 = day1.ThreeNumbersThatSumTo2020();
            Console.WriteLine($"The multiplication of three numbers gives: {threeNumbersThatSum2020.Item1 * threeNumbersThatSum2020.Item2 * threeNumbersThatSum2020.Item3}");

            var day2 = new Day02.Day02();
            Console.WriteLine($"==Day02==\nThere are {day2.CountValidPasswords()} passwords valid");
            Console.WriteLine($"There are {day2.CountValidPasswordsWithPositions()} correct passwords");

            var day3 = new Day03.Day03();
            Console.WriteLine($"==Day03==\nThere are {day3.CountTrees(1, 3)} in the way.");
            Console.WriteLine($"There are {day3.CountTrees(1, 1) * day3.CountTrees(1, 3) * day3.CountTrees(1, 5) * day3.CountTrees(1, 7) * day3.CountTrees(2, 1)} in the way.");

            var day4 = new Day04.Day04();
            Console.WriteLine($"==Day04==\nThere are {day4.CountValidPassports()} valid passports.");
            Console.WriteLine($"There are {day4.CountValidAndCorrectPassports()} valid passports.");

            var day5 = new Day05.Day05();
            Console.WriteLine($"==Day05==\nThe highest seat id is {day5.HighestSeatId()}");
            Console.WriteLine($"Your seat id is {day5.FindYourSeatId()}");

            var day6 = new Day06.Day06();
            Console.WriteLine($"==Day06==\nThe sum of the counts is {day6.SumOfYesAnswers()}");
            Console.WriteLine($"The sum of the questions to which all answered yes is {day6.SumOfEveryoneYesAnswers()}");

            var day7 = new Day07.Day07();
            Console.WriteLine($"==Day07==\nThere are {day7.CountColorBags()} different bag colors");
            Console.WriteLine($"There can be {day7.CountInsideColorBags()} bag colors");

            var day8 = new Day08.Day08();
            Console.WriteLine($"==Day08==\nThe accumulator's value is {day8.ValueInAccumulator()}");
            Console.WriteLine($"The accumulated value for the modification is {day8.AccumulatorAfterChange()}");

            var day9 = new Day09.Day09();
            var oddNumber = day9.OddNumberFromSequence();
            Console.WriteLine($"==Day09==\nThe number which doesn't follow the rule is {oddNumber}");
            Console.WriteLine($"The sum of the smallest and largest numbers is {day9.SumOfSmallestAndLargestNumbers(oddNumber)}");

            var day10 = new Day10.Day10();
            Console.WriteLine($"==Day10==\nThe multiplication of 1 jolts with 3 jolts is: {day10.Multiply1JoltWith3Jolt()}");
            Console.WriteLine($"There are {day10.CountNumberOfArrangements()} arrangements possible.");

            var day11 = new Day11.Day11();
            Console.WriteLine($"==Day11==\nThere are {day11.NumberOfOccupiedSeats(day11.CheckAdjacentSeats, 4)} seats occupied");
            Console.WriteLine($"==Day11==\nThere are {day11.NumberOfOccupiedSeats(day11.CheckAdjacentSeatsUntil, 5)} seats occupied");

            var day12 = new Day12.Day12();
            Console.WriteLine($"==Day12==\nThe Manhattan Distance is {day12.CalculateManhattanDistance()}");
            Console.WriteLine($"The Manhattan Distance is {day12.CalculateManhattanDistanceWithWayPoint()}");

            var day13 = new Day13.Day13();
            Console.WriteLine($"==Day13==\nThe multiplication gives {day13.MultiplyBusIdWithMinutesToWait()}");
            Console.WriteLine($"The earliest timestamp is {day13.EarliestTimestamp()}");

            var day14 = new Day14.Day14();
            Console.WriteLine($"==Day14==\nSum Values in memory is {day14.SumValuesInMemory()}");
            Console.WriteLine($"The sum of all possible numbers is {day14.SumAllValuesInMemory()}");

            var day15 = new Day15.Day15();
            Console.WriteLine($"==Day15==\nThe 2020th number is: {day15.NthNumberSpoken(2020)}");
            Console.WriteLine($"The 30000000th number is: {day15.NthNumberSpoken(30000000)}");
            Console.ReadKey();
        }
    }
}
