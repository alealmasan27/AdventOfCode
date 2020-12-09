using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day06
{
    internal class Day06
    {
        private static List<string> _yesAsQuestionAnswer;

        public Day06()
        {
            _yesAsQuestionAnswer = ReadData();
        }

        private static List<string> ReadData()
        {
            var lines = "";
            var yesAsQuestionAnswer = new List<string>();
            using var stream = new StreamReader("Day06.txt");
            while (stream.Peek() >= 0)
            {
                var line = stream.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    yesAsQuestionAnswer.Add(lines);
                    lines = "";
                }
                else
                {
                    lines = $"{lines} {line}";
                }

            }
            yesAsQuestionAnswer.Add(lines);

            return yesAsQuestionAnswer;
        }

        public int SumOfYesAnswers()
        {
            var sum = 0;
            foreach (var answer in _yesAsQuestionAnswer)
            {
                var answerSet = new HashSet<char>();
                foreach (var charInAnswer in answer.Where(charInAnswer => charInAnswer != ' '))
                {
                    answerSet.Add(charInAnswer);
                }

                sum += answerSet.Count;
            }
            return sum;
        }

        public int SumOfEveryoneYesAnswers()
        {
            var sum = 0;
            foreach (var answerGroup in _yesAsQuestionAnswer)
            {
                var differentAnswers = answerGroup.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList();
                var personAnswers = new List<char>();
                var maxCommonChars = differentAnswers.Count;
                foreach (var answer in differentAnswers)
                {
                    personAnswers.AddRange(answer.ToCharArray().ToList());
                }

                sum += personAnswers.GroupBy(x => x).Where(x => x.Count() == maxCommonChars).ToList().Count;
            }
            return sum;
        }
    }
}
