using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day10 : TestBase
    {
        [Test]
        public void Task()
        {
            var ratings = ReadLines().Select(int.Parse).OrderBy(r => r);
            var previousRating = 0;
            var deltaCounts = new[]{0, 0, 0, 1};
            foreach (var rating in ratings)
            {
                var delta = rating - previousRating;
                previousRating = rating;
                if (delta < 1 || delta > 3)
                    throw new Exception();
                deltaCounts[delta]++;
            }

            Console.WriteLine(deltaCounts[1] * deltaCounts[3]);
        }
    }
}