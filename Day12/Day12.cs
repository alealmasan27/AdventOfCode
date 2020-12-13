using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day12
{
    internal class Day12
    {
        private static List<NavigationInstruction> _navigationInstructions;
        private static readonly Dictionary<Action, Action> _leftDegreeActions = new Dictionary<Action, Action>
        {
            {Action.E, Action.N},{Action.N, Action.W}, {Action.W, Action.S}, {Action.S, Action.E} 
        };
        private static readonly Dictionary<Action, Action> _rightDegreeActions = new Dictionary<Action, Action>
        {
            {Action.E, Action.S},{Action.S, Action.W}, {Action.W, Action.N}, {Action.N, Action.E}
        };

        public Day12()
        {
            _navigationInstructions = new List<NavigationInstruction>();
            ReadData();
        }

        private static void ReadData()
        {
            foreach (var line in File.ReadAllLines("Day12.txt"))
            {
                _navigationInstructions.Add(new NavigationInstruction
                {
                    Action = Enum.Parse<Action>(line[0].ToString()), 
                    Value = Convert.ToInt32(line[1..])
                });
            }
        }

        public long CalculateManhattanDistance()
        {
            (Action, int, int) position = (Action.E, 0, 0);
            position = _navigationInstructions.Aggregate(position, (current, navigationInstruction) => navigationInstruction.Action switch
            {
                Action.N => Navigate(current, navigationInstruction.Value, navigationInstruction.Action),
                Action.E => Navigate(current, navigationInstruction.Value, navigationInstruction.Action),
                Action.S => Navigate(current, navigationInstruction.Value, navigationInstruction.Action),
                Action.W => Navigate(current, navigationInstruction.Value, navigationInstruction.Action),
                Action.F => Navigate(current, navigationInstruction.Value, current.Item1),
                Action.L => ChangeDirection(current, navigationInstruction),
                Action.R => ChangeDirection(current, navigationInstruction),
                _ => current
            });
            return Math.Abs(position.Item2) + Math.Abs(position.Item3);
        }

        public long CalculateManhattanDistanceWithWayPoint()
        {
            (Action, int, int) position = (Action.E, 0, 0);
            (Action, int, int) wayPoint = (Action.E, 10, 1);
            foreach (var navigationInstruction in _navigationInstructions)
            {
                switch (navigationInstruction.Action)
                {
                    case Action.N:
                        wayPoint = Navigate(wayPoint, navigationInstruction.Value, navigationInstruction.Action);
                        break;
                    case Action.E:
                        wayPoint = Navigate(wayPoint, navigationInstruction.Value, navigationInstruction.Action);
                        break;
                    case Action.S:
                        wayPoint = Navigate(wayPoint, navigationInstruction.Value, navigationInstruction.Action);
                        break;
                    case Action.W:
                        wayPoint = Navigate(wayPoint, navigationInstruction.Value, navigationInstruction.Action);
                        break;
                    case Action.F:
                        position = (position.Item1, position.Item2 + navigationInstruction.Value * wayPoint.Item2,
                            position.Item3 + navigationInstruction.Value * wayPoint.Item3);
                        break;
                    case Action.L:
                        wayPoint = ChangeDirectionWayPoint(wayPoint, navigationInstruction);
                        break;
                   case Action.R:
                       wayPoint = ChangeDirectionWayPoint(wayPoint, navigationInstruction);
                       break;
                };
            }
            return Math.Abs(position.Item2) + Math.Abs(position.Item3);
        }

        private static (Action, int, int) ChangeDirectionWayPoint((Action, int, int) wayPoint, NavigationInstruction navigationInstruction)
        {
            var value = navigationInstruction.Value;
            switch (navigationInstruction.Action)
            {
                case Action.L:
                    while (value > 0)
                    {
                        wayPoint = (_leftDegreeActions[wayPoint.Item1], -wayPoint.Item3, wayPoint.Item2);
                        value -= 90;
                    }
                    break;
                case Action.R:
                    while (value > 0)
                    {
                        wayPoint = (_rightDegreeActions[wayPoint.Item1], wayPoint.Item3, -wayPoint.Item2);
                        value -= 90;
                    }
                    break;
            }
            return wayPoint;
        }

        private static (Action, int, int) ChangeDirection((Action, int, int) position, NavigationInstruction navigationInstruction)
        {
            var value = navigationInstruction.Value;
            switch (navigationInstruction.Action)
            {
                case Action.L:
                    while (value > 0)
                    {
                        position.Item1 = _leftDegreeActions[position.Item1];
                        value -= 90;
                    }
                    break;
                case Action.R:
                    while (value > 0)
                    {
                        position.Item1 = _rightDegreeActions[position.Item1];
                        value -= 90;
                    }
                    break;
            }
            return position;
        }

        private static (Action, int, int) Navigate((Action, int, int) position, int value, Action action)
        {
            return action switch
            {
                Action.N => (position.Item1, position.Item2, position.Item3 + value),
                Action.E => (position.Item1, position.Item2 + value, position.Item3),
                Action.S => (position.Item1, position.Item2, position.Item3 - value),
                Action.W => (position.Item1, position.Item2 - value, position.Item3),
                _ => position
            };
        }
    }
}
