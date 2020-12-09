using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day04
{
    public class Passport
    {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }

        private List<string> EyeColor = new List<string> { "amb","blu", "brn", "gry", "grn", "hzl", "oth" };
        public bool ByrIsValid()
        {
            var byr = Convert.ToInt32(Byr);
            return byr >= 1920 && byr <= 2002;
        }

        public bool IyrIsValid()
        {
            var iyr = Convert.ToInt32(Iyr);
            return iyr >= 2010 && iyr <= 2020;
        }

        public bool EyrIsValid()
        {
            var eyr = Convert.ToInt32(Eyr);
            return eyr >= 2020 && eyr <= 2030;
        }

        public bool HgtIsValid()
        {
            var unitOfMeasureStartPosition = Hgt.Length - 2;
            var unitOfMeasure = Hgt[unitOfMeasureStartPosition..];
            int.TryParse(Hgt[0..^2], out var number);
            return unitOfMeasure switch
            {
                "cm" => number >= 150 && number <= 193,
                "in" => number >= 59 && number <= 76,
                _ => false
            };
        }

        public bool HclIsValid()
        {
            var pattern = @"#[0-9a-f]{6}";
            var regex = new Regex(pattern);
            return regex.Matches(Hcl).Count == 1;
        }

        public bool EclIsValid()
        {
            return EyeColor.Contains(Ecl);
        }

        public bool PidIsValid()
        {
            if (Pid.Any(c => c < '0' || c > '9'))
            {
                return false;
            }

            return Pid.Length == 9;
        }

        public bool AllPropertiesAreValid()
        {
            return ByrIsValid() && IyrIsValid() && EyrIsValid() && HgtIsValid() && HclIsValid() && EclIsValid() &&
                   PidIsValid();
        }
    }
}
