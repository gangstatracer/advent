using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day17 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var space = ReadLines()
                .Select((s, i) => s.Select((c, j) => (X: i, Y: j, Active: c == '#')))
                .SelectMany(t => t)
                .Where(t => t.Active)
                .Select(t => (t.X, t.Y, Z: 0, W: 0))
                .ToHashSet();

            for (var i = 0; i < 6; i++)
                space = Life(space);
            Console.WriteLine(space.Count);
        }

        private static HashSet<(int, int, int, int)> Life(HashSet<(int, int, int, int)> active)
        {
            var activeNeighborsCounts = active
                .ToDictionary(c => c, c => (active: true, activeNeighbors: 0));

            foreach (var neighbor in active.SelectMany(GetNeighbors))
            {
                if (!activeNeighborsCounts.ContainsKey(neighbor))
                    activeNeighborsCounts[neighbor] = (active: false, activeNeighbors: 0);
                var (isActive, count) = activeNeighborsCounts[neighbor];
                activeNeighborsCounts[neighbor] = (isActive, count + 1);
            }

            return activeNeighborsCounts
                .Where(kvp => kvp.Value.active switch
                {
                    true => kvp.Value.activeNeighbors == 2 || kvp.Value.activeNeighbors == 3,
                    false => kvp.Value.activeNeighbors == 3
                })
                .Select(kvp => kvp.Key)
                .ToHashSet();
        }

        private static IEnumerable<(int, int, int, int)> GetNeighbors((int X, int Y, int Z, int W) cell)
        {
            var (x, y, z, w) = cell;
            for (var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
            for (var k = -1; k <= 1; k++)
            for (var l = -1; l <= 1; l++)
                if (i != 0 || j != 0 || k != 0 || l != 0)
                    yield return (x + i, y + j, z + k, w + l);
        }
    }
}