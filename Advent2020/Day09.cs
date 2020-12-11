using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day09 : TestBase
    {
        [Test]
        public void Task()
        {
            var numbers = ReadLines().Select(long.Parse).ToArray();
            var odd = FindOdd(25, numbers);
            Console.WriteLine(odd);
            var weakness = FindWeakness(odd, numbers);
            Console.WriteLine(weakness);
        }

        [Test]
        public void TestExample()
        {
            var numbers = new long[]
            {
                35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            };
            Assert.AreEqual(127, FindOdd(5, numbers));
            Assert.AreEqual(62, FindWeakness(127, numbers));
        }

        private static long FindWeakness(long odd, IReadOnlyList<long> numbers)
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                var sum = numbers[i];
                var min = numbers[i];
                var max = numbers[i];
                for (var j = i + 1; j < numbers.Count; j++)
                {
                    min = Math.Min(min, numbers[j]);
                    max = Math.Max(max, numbers[j]);
                    sum = checked(sum + numbers[j]);

                    if (sum > odd)
                        continue;

                    if (sum == odd)
                        return min + max;
                }
            }

            throw new Exception();
        }

        private static long FindOdd(int preamble, IReadOnlyList<long> numbers)
        {
            var sums = new long[preamble, preamble];
            for (var i = 0; i < preamble; i++)
            for (var j = 0; j < preamble; j++)
            {
                sums[i, j] = numbers[i] + numbers[j];
            }

            for (var index = preamble; index < numbers.Count; index++)
            {
                if (!Contains(sums, numbers[index]))
                    return numbers[index];

                for (var i = 0; i < preamble; i++)
                {
                    sums[index % preamble, i] -= numbers[index - preamble];
                    sums[index % preamble, i] += numbers[index];

                    sums[i, index % preamble] = sums[index % preamble, i];
                }
            }

            throw new Exception();
        }

        private static bool Contains(long[,] searchTarget, long number)
        {
            for (var i = 0; i < searchTarget.GetLength(0); i++)
            for (var j = i; j < searchTarget.GetLength(1); j++)
                if (searchTarget[i, j] == number)
                    return true;
            return false;
        }
    }
}