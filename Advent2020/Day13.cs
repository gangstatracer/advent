using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day13 : TestBase
    {
        [Test]
        public void Task()
        {
            var lines = ReadLines().ToArray();
            var minDesired = int.Parse(lines[0]);
            var ids = lines[1]
                .Split(",")
                .Select((s, i) => (s, i))
                .Where(t => t.s != "x")
                .Select(t => (int.Parse(t.s), t.i))
                .ToArray();

            var minScheduled = long.MaxValue;
            var task1 = 0L;
            foreach (var (id, _) in ids)
            {
                var closest = (long) Math.Ceiling((double) minDesired / id) * id;
                if (closest < minScheduled)
                {
                    minScheduled = closest;
                    task1 = id * (minScheduled - minDesired);
                }
            }

            Console.WriteLine(task1);
            Console.WriteLine(Task2(ids));
        }

        [Test]
        public void Example()
        {
            Assert.AreEqual(3417, Task2(new[] {(17, 0), (13, 2), (19, 3)}));
        }

        private static long Task2(IList<(int Id, int Offset)> ids)
        {
            var timestamp = 0L;
            while (true)
            {
                timestamp++;
                var success = true;

                foreach (var (id, offset) in ids)
                    if ((timestamp + offset) % id != 0)
                        success = false;
                if (success)
                    return timestamp;

                if (timestamp % 1_000_000 == 0)
                    TestContext.Progress.WriteLine(timestamp);
            }
        }

        [Test]
        public void TestGcd()
        {
            Assert.AreEqual((1, 17, -12), Gcd(29, 41));
            Assert.AreEqual((1, 0, 1), Gcd(0, 41));
        }

        private static (int result, int x, int y) Gcd(int a, int b)
        {
            if (a == 0)
                return (b, 0, 1);
            var (d, x1, y1) = Gcd(b % a, a);
            return (d, y1 - b / a * x1, x1);
        }
    }
}