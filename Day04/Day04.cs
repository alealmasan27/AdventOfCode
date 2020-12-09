using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day04
{
    internal class Day04
    {
        private readonly List<Passport> _passports;

        public Day04()
        {
            _passports = ReadData();
        }

        private static Passport NewPassport(string passportLines)
        {
            var passport = new Passport();
            var passportFields = passportLines.Split(" ").Where(x => !string.IsNullOrEmpty(x));
            foreach (var field in passportFields)
            {
                switch (field.Substring(0, 3))
                {
                    case "byr":
                        passport.Byr = field[4..];
                        break;
                    case "iyr":
                        passport.Iyr = field[4..];
                        break;
                    case "eyr":
                        passport.Eyr = field[4..];
                        break;
                    case "hgt":
                        passport.Hgt = field[4..];
                        break;
                    case "hcl":
                        passport.Hcl = field[4..];
                        break;
                    case "ecl":
                        passport.Ecl = field[4..];
                        break;
                    case "pid":
                        passport.Pid = field[4..];
                        break;
                    case "cid":
                        passport.Cid = field[4..];
                        break;

                }
            }
            return passport;
        }

        private static List<Passport> ReadData()
        {
            var list = new List<Passport>();
            var lines = "";
            using var stream = new StreamReader("Day04.txt");
            while (stream.Peek() >= 0)
            {
                var line = stream.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    list.Add(NewPassport(lines));
                    lines = "";
                }
                else
                {
                    lines = $"{lines} {line}";
                }

            }
            list.Add(NewPassport(lines));

            return list;
        }

        public long CountValidPassports()
        {
            return _passports.LongCount(HasAllProperties);
        }

        public long CountValidAndCorrectPassports()
        {
            return _passports.LongCount(HasAllPropertiesAndAreValid);
        }

        private static bool HasAllPropertiesAndAreValid(Passport passport)
        {
            return HasAllProperties(passport) && passport.AllPropertiesAreValid();
        }

        private static bool HasAllProperties(Passport passport)
        {
            return !string.IsNullOrEmpty(passport.Hgt) && !string.IsNullOrEmpty(passport.Byr) &&
                   !string.IsNullOrEmpty(passport.Ecl) && !string.IsNullOrEmpty(passport.Eyr) &&
                   !string.IsNullOrEmpty(passport.Hcl) && !string.IsNullOrEmpty(passport.Iyr) &&
                   !string.IsNullOrEmpty(passport.Pid);
        }
    }
}
