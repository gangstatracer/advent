using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day7 : TestBase
    {
        [Test]
        public void Task()
        {
            var rules = ReadLines()
                .Select(ParseRule)
                .ToDictionary(r => r.key, r => r.requiremets);

            Console.WriteLine(rules.Keys.Count(k => CanContainColor(rules, k, "shiny gold")));

            Console.WriteLine(ShouldContain(rules, "shiny gold"));
        }

        [Test]
        public void TestExample()
        {
            var rules = new[]
                {
                    "shiny gold bags contain 2 dark red bags.",
                    "dark red bags contain 2 dark orange bags.",
                    "dark orange bags contain 2 dark yellow bags.",
                    "dark yellow bags contain 2 dark green bags.",
                    "dark green bags contain 2 dark blue bags.",
                    "dark blue bags contain 2 dark violet bags.",
                    "dark violet bags contain no other bags."
                }
                .Select(ParseRule)
                .ToDictionary(r => r.key, r => r.requiremets);
            Assert.AreEqual(126, ShouldContain(rules, "shiny gold"));
        }

        private static int ShouldContain(
            IReadOnlyDictionary<string, Dictionary<string, int>> rules,
            string inspectedColor)
        {
            var bagsCount = 0;
            foreach (var (newColor, count) in rules[inspectedColor])
                bagsCount += count * (1 + ShouldContain(rules, newColor));
            return bagsCount;
        }


        private bool CanContainColor(
            IReadOnlyDictionary<string, Dictionary<string, int>> rules,
            string inspectedColor,
            string desiredColor)
        {
            if (_canContainColorCache.ContainsKey(inspectedColor))
                return _canContainColorCache[inspectedColor];

            foreach (var (newColor, _) in rules[inspectedColor])
            {
                if (newColor == desiredColor)
                {
                    _canContainColorCache[inspectedColor] = true;
                    return true;
                }

                if (CanContainColor(rules, newColor, desiredColor))
                {
                    _canContainColorCache[inspectedColor] = true;
                    return true;
                }
            }

            _canContainColorCache[inspectedColor] = false;
            return false;
        }

        private readonly Dictionary<string, bool> _canContainColorCache = new Dictionary<string, bool>();

        private static (string key, Dictionary<string, int> requiremets) ParseRule(string rule)
        {
            var tokens = rule
                .Substring(0, rule.Length - 1)
                .Replace(",", "")
                .Split(new[] {"bags", "bag", "contain"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToArray();

            var key = tokens[0];
            var requirements = new Dictionary<string, int>();
            for (var i = 1; i < tokens.Length; i++)
            {
                if (tokens[i] == "no other")
                    continue;
                var ruleTokens = tokens[i].Split(" ");
                var count = int.Parse(ruleTokens[0]);
                var color = tokens[i].Substring(ruleTokens[0].Length + 1);
                requirements[color] = count;
            }

            return (key, requirements);
        }
    }
}