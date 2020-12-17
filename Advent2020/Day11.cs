using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Advent2020
{
    public class Day11 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var grid = ReadLines().Select(s => s.ToCharArray()).ToArray();
            var result = Life(grid);
            Console.WriteLine(result);
        }

        [Test]
        public void Example()
        {
            Assert.AreEqual(
                26,
                Life(new[]
                {
                    new[] {'L', '.', 'L', 'L', '.', 'L', 'L', '.', 'L', 'L'},
                    new[] {'L', 'L', 'L', 'L', 'L', 'L', 'L', '.', 'L', 'L'},
                    new[] {'L', '.', 'L', '.', 'L', '.', '.', 'L', '.', '.'},
                    new[] {'L', 'L', 'L', 'L', '.', 'L', 'L', '.', 'L', 'L'},
                    new[] {'L', '.', 'L', 'L', '.', 'L', 'L', '.', 'L', 'L'},
                    new[] {'L', '.', 'L', 'L', 'L', 'L', 'L', '.', 'L', 'L'},
                    new[] {'.', '.', 'L', '.', 'L', '.', '.', '.', '.', '.'},
                    new[] {'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
                    new[] {'L', '.', 'L', 'L', 'L', 'L', 'L', 'L', '.', 'L'},
                    new[] {'L', '.', 'L', 'L', 'L', 'L', 'L', '.', 'L', 'L'},
                }));
        }

        private static int Life(char[][] grid)
        {
            var nextGrid = Enumerable.Range(0, grid.Length).Select(i => new char[grid[i].Length]).ToArray();

            var somethingChanged = true;
            while (somethingChanged)
            {
                somethingChanged = false;
                for (var i = 0; i < grid.Length; i++)
                for (var j = 0; j < grid[i].Length; j++)
                {
                    nextGrid[i][j] = GetOccupiedState(i, j, grid);
                    if (nextGrid[i][j] != grid[i][j])
                        somethingChanged = true;
                }

                var swap = grid;
                grid = nextGrid;
                nextGrid = swap;
                var state = WriteState(grid);
            }

            var result = grid.Sum(g => g.Count(s => s == '#'));
            return result;
        }

        private static char GetOccupiedState(int i, int j, IReadOnlyList<char[]> grid)
        {
            var value = grid[i][j];
            var adjacent = GetAdjacent(i, j, grid);
            return value switch
            {
                'L' when adjacent.All(c => c != '#') => '#',
                '#' when adjacent.Count(c => c == '#') >= 5 => 'L',
                _ => value
            };
        }

        private static IEnumerable<char> GetAdjacent(int i, int j, IReadOnlyList<char[]> grid)
        {
            for (var k = -1; k <= 1; k++)
            for (var l = -1; l <= 1; l++)
            {
                if (k == 0 && l == 0)
                    continue;
                var ii =i;
                var jj = j;
                do
                {
                    ii += k;
                    jj += l;
                } while (IndicesValid(grid, ii, jj) && grid[ii][jj] == '.');

                if (IndicesValid(grid, ii, jj))
                    yield return grid[ii][jj];
            }
        }

        private static bool IndicesValid(IReadOnlyList<char[]> grid, int ii, int jj)
        {
            return ii >= 0 && ii < grid.Count && jj >= 0 && jj < grid[ii].Length;
        }

        private static string WriteState(IEnumerable<char[]> grid)
        {
            var builder = new StringBuilder();
            foreach (var line in grid)
            {
                builder.AppendLine(string.Join("", line));
            }

            return builder.ToString();
        }
    }
}