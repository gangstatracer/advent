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

        private static readonly Dictionary<string, (int, int)> Directions = new Dictionary<string, (int, int)>
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