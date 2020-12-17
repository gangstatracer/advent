using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Advent2020
{
    public class Day04 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var passports = ReadAll()
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(p => p
                    .Split(new[] {" ", Environment.NewLine}, StringSplitOptions.None)
                    .Select(k => k.Split(':'))
                    .ToDictionary(a => a[0], a => a[1]))
                .ToArray();

            var valid = passports.Count(IsValid);
            Console.WriteLine(valid);
        }

        private static bool IsValid(Dictionary<string, string> passport)
        {
            var requiredKeys = new Dictionary<string, Func<string, bool>>
            {
                ["byr"] = s => int.TryParse(s, out var year) && year >= 1920 && year <= 2002,
                ["iyr"] = s => int.TryParse(s, out var year) && year >= 2010 && year <= 2020,
                ["eyr"] = s => int.TryParse(s, out var year) && year >= 2020 && year <= 2030,
                ["hgt"] = IsHeightValid,
                ["hcl"] = s => Regex.IsMatch(s, "^#[0-9,a-f]{6}$"),
                ["ecl"] = s => ValidEyeColors.Contains(s),
                ["pid"] = s => Regex.IsMatch(s, @"^\d{9}$"),
            };
            return requiredKeys.All(validation =>
                passport.ContainsKey(validation.Key) && validation.Value(passport[validation.Key]));
        }

        private static bool IsHeightValid(string value)
        {
            if (!Regex.IsMatch(value, @"^\d+\w{2}$"))
                return false;

            var unit = value.Substring(value.Length - 2);
            var number = int.Parse(value.Substring(0, value.Length - 2));
            return unit switch
            {
                "cm" => number >= 150 && number <= 193,
                "in" => number >= 59 && number <= 76,
                _ => false,
            };
        }

        private static readonly HashSet<string> ValidEyeColors = new HashSet<string>
            {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
    }
}