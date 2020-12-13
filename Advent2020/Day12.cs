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
            var waypoint = (X: 1, Y: 10);
            foreach (var (instruction, argument) in instructions)
            {
                switch (instruction)
                {
                    case 'N':
                        waypoint.X += argument;
                        break;
                    case 'E':
                        waypoint.Y += argument;
                        break;
                    case 'S':
                        waypoint.X -= argument;
                        break;
                    case 'W':
                        waypoint.Y -= argument;
                        break;
                    case 'L':
                        waypoint = RotateWaypoint(waypoint, argument, -1);
                        break;
                    case 'R':
                        waypoint = RotateWaypoint(waypoint, argument, +1);
                        break;
                    case 'F':
                        x += argument * waypoint.X;
                        y += argument * waypoint.Y;
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        private static (int X, int Y) RotateWaypoint((int X, int Y) waypoint, int degrees, int sign)
        {
            for (var i = 0; i < degrees % 360 / 90; i++)
                waypoint = (sign * -1 * waypoint.Y, sign * waypoint.X);

            return waypoint;
        }

        private static char RotateShip(char direction, int degrees, int sign)
        {
            if (degrees % 90 != 0)
                throw new Exception();

            var shift = (Circle.IndexOf(direction) + degrees / 90 * sign) % 4;
            if (shift < 0)
                shift += 4;

            return Circle[shift];
        }

        private static readonly List<char> Circle = new List<char> {'N', 'E', 'S', 'W'};
    }
}