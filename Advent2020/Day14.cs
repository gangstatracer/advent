using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day14 : TestBase
    {
        [Test]
        public void Task()
        {
            var lines = ReadLines().Select(l =>
            {
                var parts = l.Split(" = ");
                return (Variable: parts[0], Value: parts[1]);
            });
            var mask = "";
            var mem = new Dictionary<int, long>();
            foreach (var (variable, value) in lines)
            {
                if (variable == "mask")
                    mask = value;
                else
                {
                    var actualValue = ApplyMask(long.Parse(value), mask);
                    var address = variable.Substring(4, variable.Length - 5);
                    mem[int.Parse(address)] = actualValue;
                }
            }

            Console.WriteLine(mem.Values.Sum());
        }

        private static long ApplyMask(long value, string mask)
        {
            // Console.WriteLine($"Mask: {mask}");
            // Console.WriteLine($"Value: {Convert.ToString(value, 2)}");
            const long inversion = 0b111111111111111111111111111111111111;
            var power = 1L;
            for (var i = mask.Length - 1; i >= 0; i--)
            {
                if (mask[i] == '1')
                {
                    value |= power;
                }

                if (mask[i] == '0')
                {
                    value &= inversion ^ power;
                }

                power *= 2;
            }


            // Console.WriteLine($"Result: {Convert.ToString(value, 2)}");
            return value;
        }
    }
}