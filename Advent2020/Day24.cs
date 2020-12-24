using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day24 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var instructions = ReadLines();
            var floor = new Dictionary<(int, int), bool>();
            Flip(floor, instructions);
            Console.WriteLine(floor.Values.Count(t => t));

            var blackTiles = floor
                .Where(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToHashSet();
            for (var i = 0; i < 100; i++)
            {
                blackTiles = Life(blackTiles).ToHashSet();
                TestContext.Progress.WriteLine($"Day {i + 1}: {blackTiles.Count}");
            }

            Console.WriteLine(blackTiles.Count);
        }

        [Test]
        public void Example()
        {
            var floor = new Dictionary<(int, int), bool>();
            var instructions = new[]
            {
                "sesenwnenenewseeswwswswwnenewsewsw",
                "neeenesenwnwwswnenewnwwsewnenwseswesw",
                "seswneswswsenwwnwse",
                "nwnwneseeswswnenewneswwnewseswneseene",
                "swweswneswnenwsewnwneneseenw",
                "eesenwseswswnenwswnwnwsewwnwsene",
                "sewnenenenesenwsewnenwwwse",
                "wenwwweseeeweswwwnwwe",
                "wsweesenenewnwwnwsenewsenwwsesesenwne",
                "neeswseenwwswnwswswnw",
                "nenwswwsewswnenenewsenwsenwnesesenew",
                "enewnwewneswsewnwswenweswnenwsenwsw",
                "sweneswneswneneenwnewenewwneswswnese",
                "swwesenesewenwneswnwwneseswwne",
                "enesenwswwswneneswsenwnewswseenwsese",
                "wnwnesenesenenwwnenwsewesewsesesew",
                "nenewswnwewswnenesenwnesewesw",
                "eneswnwswnwsenenwnwnwwseeswneewsenese",
                "neswnwewnwnwseenwseesewsenwsweewe",
                "wseweeenwnesenwwwswnew"
            };
            Flip(floor, instructions);
            Assert.AreEqual(10, floor.Values.Count(t => t));

            var blackTiles = floor
                .Where(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToHashSet();
            for (var i = 0; i < 100; i++)
            {
                blackTiles = Life(blackTiles).ToHashSet();
                TestContext.Progress.WriteLine($"Day {i + 1}: {blackTiles.Count}");
            }

            Assert.AreEqual(2208, blackTiles.Count);
        }

        private static void Flip(IDictionary<(int, int), bool> floor, IEnumerable<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                var i = 0;
                var (x, y) = (0, 0);
                while (i < instruction.Length)
                {
                    var direction = "";
                    do
                    {
                        direction += instruction[i];
                        i++;
                    } while (!Directions.ContainsKey(direction));

                    var (offsetX, offsetY) = Directions[direction];
                    x += offsetX;
                    y += offsetY;
                }

                floor[(x, y)] = !floor.ContainsKey((x, y)) || !floor[(x, y)];
            }
        }

        private static IEnumerable<(int, int)> Life(ISet<(int X, int Y)> blackTiles)
        {
            var floor = blackTiles.ToDictionary(
                t => t,
                t => (blackNeighbors: 0, color: true));
            foreach (var neighbor in blackTiles
                .SelectMany(t => Directions
                    .Values
                    .Select(offset => (t.X + offset.X, t.Y + offset.Y))))
            {
                if (!floor.ContainsKey(neighbor))
                    floor[neighbor] = (blackNeighbors: 0, color: false);
                var (blackNeighbors, color) = floor[neighbor];
                floor[neighbor] = (blackNeighbors + 1, color);
            }

            return floor
                .Where(kvp => kvp.Value.color switch
                {
                    true => kvp.Value.blackNeighbors > 0 && kvp.Value.blackNeighbors < 3,
                    false => kvp.Value.blackNeighbors == 2
                })
                .Select(kvp => kvp.Key);
        }

        private static readonly Dictionary<string, (int X, int Y)> Directions = new Dictionary<string, (int, int)>
        {
            {"e", (2, 0)},
            {"se", (1, -1)},
            {"sw", (-1, -1)},
            {"w", (-2, 0)},
            {"nw", (-1, 1)},
            {"ne", (1, 1)},
        };
    }
}