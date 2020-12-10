using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day10 : TestBase
    {
        [Test]
        public void Task()
        {
            var ratings = ReadLines().Select(int.Parse);
            var (deltas, variations) = Solve(ratings);

            Console.WriteLine(deltas);
            Console.WriteLine(variations);
        }

        [Test]
        public void Example1()
        {
            var (deltas, variations) = Solve(new[] {16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4});
            Assert.AreEqual(35, deltas);
            Assert.AreEqual(8, variations);
        }

        [Test]
        public void Example2()
        {
            var (deltas, variations) = Solve(new[]
            {
                28, 33, 18, 42, 31, 14, 46, 20, 48, 47,
                24, 23, 49, 45, 19, 38, 39, 11, 1, 32,
                25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3,
            });
            Assert.AreEqual(220, deltas);
            Assert.AreEqual(19208, variations);
        }

        private static (int deltas, long variations) Solve(IEnumerable<int> ratings)
        {
            var previousRating = 0;
            var deltaCounts = new[] {0, 0, 0, 1};
            var variations = 1L;
            var continuousSegmentLength = 1;
            foreach (var rating in ratings.OrderBy(r => r))
            {
                var delta = rating - previousRating;
                previousRating = rating;
                if (delta < 1 || delta > 3)
                    throw new Exception();
                deltaCounts[delta]++;
                if (delta == 3)
                {
                    variations *= CountVariations(continuousSegmentLength);
                    continuousSegmentLength = 1;
                }
                else
                {
                    continuousSegmentLength++;
                }
            }

            return (deltaCounts[1] * deltaCounts[3], variations * CountVariations(continuousSegmentLength));
        }

        private static int CountVariations(int continuousSegmentLength)
        {
            return continuousSegmentLength switch
            {
                1 => 1,
                2 => 1,
                3 => 2,
                4 => 4,
                5 => 7,
                _ => throw new Exception()
            };
        }
    }
}