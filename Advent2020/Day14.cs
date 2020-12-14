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
            var mem = new Dictionary<long, long>();
            foreach (var (variable, value) in lines)
            {
                if (variable == "mask")
                    mask = value;
                else
                {
                    var address = variable.Substring(4, variable.Length - 5);
                    var val = long.Parse(value);
                    foreach (var addr in ApplyMask2(int.Parse(address), mask))
                        mem[addr] = val;
                }
            }

            Console.WriteLine(mem.Values.Sum());
        }

        private static long ApplyMask1(long value, string mask)
        {
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

            return value;
        }

        private static IEnumerable<long> ApplyMask2(int value, string mask)
        {
            var power = mask.Count(c => c == 'X');
            for (var i = 0; i < Math.Pow(2, power); i++)
            {
                yield return Combine(value, i, mask);
            }
        }

        private static long Combine(long source, int placeholders, string mask)
        {
            var result = 0L;
            for (var i = mask.Length - 1; i >= 0; i--)
            {
                result *= 2;
                if (mask[i] == 'X')
                {
                    result += placeholders % 2;
                    placeholders /= 2;
                }
                else
                {
                    result += mask[i] == '0' ? (int) source % 2 : 1;
                }

                source /= 2;
            }

            result = Reverse(result);
            return result;
        }

        private static long Reverse(long value)
        {
            var result = 0L;
            for (var i = 0; i < 36; i++)
            {
                result *= 2;
                result += value % 2;
                value /= 2;
            }

            return result;
        }
    }
}