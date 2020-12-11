using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day06 : TestBase
    {
        [Test]
        public void Task()
        {
            var groups = ReadAll()
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(EveryOneYes)
                .Sum();

            Console.WriteLine(groups);
        }

        private int AnyOneYes(string group)
        {
            return group.Replace(Environment.NewLine, string.Empty)
                .Distinct()
                .Count();
        }

        private static int EveryOneYes(string group)
        {
            var members = group.Split(Environment.NewLine).Select(m => m.ToHashSet());
            var result = Enumerable.Range(0, 26).Select(i => (char) ('a' + i)).ToHashSet();
            foreach (var member in members) result.IntersectWith(member);

            return result.Count;
        }
    }
}