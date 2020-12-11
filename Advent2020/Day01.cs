using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day01 : TestBase
    {
        [Test]
        public void Task()
        {
            var numbers = ReadLines().Select(int.Parse).ToArray();
            for (var i = 0; i < numbers.Length; i++)
            for (var j = i; j < numbers.Length; j++)
            for (var k = j; k < numbers.Length; k++)
                if (numbers[i] + numbers[j] + numbers[k] == 2020)
                {
                    Console.WriteLine(numbers[i] * numbers[j] * numbers[k]);
                    return;
                }
        }
    }
}