using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day15 : TestBase
    {
        [Test]
        public void Task()
        {
            var startNumbers = new[] {16, 1, 0, 18, 12, 14, 19};
            Console.WriteLine(Play(startNumbers, 2020));
            Console.WriteLine(Play(startNumbers, 30_000_000));
        }

        [Test]
        public void Example()
        {
            Assert.AreEqual(175594, Play(new []{0,3,6}, 30_000_000));
        }

        private static int Play(IReadOnlyCollection<int> startNumbers, int stop)
        {
            var spoken = startNumbers
                .Select((n, i) => (Number: n, Index: i))
                .ToDictionary(
                    t => t.Number,
                    t => (Current: t.Index, Previous: t.Index));

            var previous = startNumbers.Last();
            for (var i = startNumbers.Count; i < stop; i++)
            {
                var (cur, prev) = spoken[previous];
                var next = cur - prev;
                if (!spoken.ContainsKey(next))
                {
                    spoken[next] = (i, i);
                }
                else
                {
                    spoken[next] = (i, spoken[next].Current);
                }

                previous = next;
            }

            return previous;
        }
    }
}