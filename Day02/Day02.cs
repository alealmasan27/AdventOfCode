using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day02
{
    internal class Day02
    {
        private readonly List<PasswordPolicy> _listOfPasswords;

        public Day02()
        {
            _listOfPasswords = ReadData();
        }

        private static PasswordPolicy NewPasswordPolicy(int passMinCount, int passMaxCount, char charToSearch, string password)
        {
            return new PasswordPolicy
            {
                PasswordCharMin = passMinCount,
                PasswordCharMax = passMaxCount,
                Char = charToSearch,
                Password = password
            };
        }

        public List<PasswordPolicy> ReadData()
        {
            var list = new List<PasswordPolicy>();
            using var stream = new StreamReader("Day02.txt");
            while (stream.Peek() >= 0)
            {
                var line = stream.ReadLine();
                if (line == null) continue;
                var lineSplit = line.Split(" ");
                var minMaxValues = lineSplit[0].Split("-");
                list.Add(NewPasswordPolicy(Convert.ToInt32(minMaxValues[0]),Convert.ToInt32(minMaxValues[1]), lineSplit[1][0], lineSplit[2]));

            }

            return list;
        }

        private static bool PasswordMeetsThePolicy(PasswordPolicy passwordPolicy)
        {
            var charCount = passwordPolicy.Password.Count(a => passwordPolicy.Char == a);
            return charCount >= passwordPolicy.PasswordCharMin && charCount <= passwordPolicy.PasswordCharMax;
        }

        private static bool PasswordMeetsTheCorrectPolicy(PasswordPolicy passwordPolicy)
        {
            return (passwordPolicy.Password[passwordPolicy.PasswordCharMin - 1] == passwordPolicy.Char && passwordPolicy.Password[passwordPolicy.PasswordCharMax - 1] != passwordPolicy.Char) || 
                   (passwordPolicy.Password[passwordPolicy.PasswordCharMin - 1] != passwordPolicy.Char && passwordPolicy.Password[passwordPolicy.PasswordCharMax - 1] == passwordPolicy.Char);
        }

        public int CountValidPasswords()
        {
            return _listOfPasswords.Count(PasswordMeetsThePolicy);
        }

        public int CountValidPasswordsWithPositions()
        {
            return _listOfPasswords.Count(PasswordMeetsTheCorrectPolicy);
        }
    }
}
