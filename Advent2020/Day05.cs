using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day05 : TestBase
    {
        [Test]
        public void Task()
        {
            var passes = ReadLines().Select(GetPassId).OrderBy(x => x).ToArray();
            for (var i = 1; i < passes.Length; i++)
                if (passes[i] - passes[i - 1] > 1)
                    Console.WriteLine($"{passes[i]} {passes[i - 1]}");
        }

        [TestCase("BFFFBBFRRR", 567)]
        [TestCase("FFFBBBFRRR", 119)]
        [TestCase("BBFFBBFRLL", 820)]
        [TestCase("FFFFFFFLLL", 0)]
        [TestCase("BBBBBBBRRR", 1023)]
        public void TestGetPassId(string code, int id)
        {
            Assert.AreEqual(id, GetPassId(code));
        }

        private static int GetPassId(string code)
        {
            var row = new Range(0, 127);
            for (var i = 0; i < 7; i++)
                row = code[i] switch
                {
                    'F' => row.Lower(),
                    'B' => row.Upper(),
                    _ => throw new Exception()
                };
            if (row.Length > 0)
                throw new Exception();

            var column = new Range(0, 7);
            for (var i = 7; i < 10; i++)
                column = code[i] switch
                {
                    'L' => column.Lower(),
                    'R' => column.Upper(),
                    _ => throw new Exception()
                };
            if (column.Length > 0)
                throw new Exception();

            return row.Left * 8 + column.Left;
        }

        private class Range
        {
            public Range(int left, int right)
            {
                Left = left;
                Right = right;
            }

            public int Left { get; set; }
            public int Right { get; set; }
            public int Length => Right - Left;

            public Range Lower() => new Range(Left, Left + Length / 2);
            public Range Upper() => new Range(Left + Length / 2 + 1, Right);

            public override string ToString()
            {
                return $"[{Left}, {Right}]";
            }
        }
    }
}