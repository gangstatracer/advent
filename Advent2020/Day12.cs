using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day12 : TestBase
    {
        [Test]
        public void Task()
        {
            var instructions = ReadLines()
                .Select(l => (Direction: l[0], Argument: int.Parse(l.Substring(1))))
                .ToArray();
            int x = 0, y = 0;
            var facing = 'E';
            foreach (var (instruction, argument) in instructions)
            {
                var direction = instruction;
                if (instruction == 'F')
                    direction = facing;

                switch (direction)
                {
                    case 'N':
                        x += argument;
                        break;
                    case 'E':
                        y += argument;
                        break;
                    case 'S':
                        x -= argument;
                        break;
                    case 'W':
                        y -= argument;
                        break;
                    case 'L':
                        facing = RotateShip(facing, argument, -1);
                        break;
                    case 'R':
                        facing = RotateShip(facing, argument, +1);
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        private static char RotateShip(char direction, int degrees, int sign)
        {
            if (degrees % 90 != 0)
                throw new Exception();

            var shift = (Circle.IndexOf(direction) + degrees / 90 * sign) % 4;
            if (shift < 0)
                shift += 4;
            if (shift < 0 || shift > 3)
                throw new Exception();
            return Circle[shift];
        }

        private static readonly List<char> Circle = new List<char> {'N', 'E', 'S', 'W'};
    }
}