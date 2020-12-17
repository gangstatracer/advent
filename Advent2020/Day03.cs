using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day03 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var map = ReadLines()
                .Select(l => l
                    .Select(c => c switch
                    {
                        '.' => false,
                        '#' => true,
                        _ => throw new Exception()
                    })
                    .ToArray())
                .ToArray();

            var result = new (int right, int down)[]
            {
                (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)
            }.Aggregate(1L,
                (r, t) => r * Slope(map, t.right, t.down));


            Console.WriteLine(result);
        }

        private static int Slope(bool[][] map, int right, int down)
        {
            int i = 0, j = 0, trees = 0;
            while (i < map.Length - down)
            {
                i += down;
                j += right;

                var relativeJ = j % map[i].Length;
                if (map[i][relativeJ])
                    trees++;
            }

            return trees;
        }
    }
}