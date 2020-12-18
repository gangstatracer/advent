using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day18 : DayTestBase
    {
        [Test]
        public void Task()
        {
            Console.WriteLine(ReadLines().Select(Process).Sum());
        }

        private static long Process(string expression)
        {
            expression = expression.Replace(" ", "");
            var values = new Stack<long>();
            var operations = new Stack<char>();
            for (var i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '(':
                        operations.Push(expression[i]);
                        break;
                    case ')':
                    {
                        while (operations.Peek() != '(')
                            ProcessOperation(values, operations.Pop());
                        operations.Pop();
                        break;
                    }
                    case '+':
                    case '*':
                    {
                        var currentOperation = expression[i];
                        while (operations.Any() && Priority(operations.Peek()) >= Priority(currentOperation))
                            ProcessOperation(values, operations.Pop());
                        operations.Push(currentOperation);
                        break;
                    }
                    default:
                    {
                        var start = i;
                        while (i < expression.Length && char.IsLetterOrDigit(expression[i]))
                            i++;
                        i--;
                        var value = expression.Substring(start, i - start + 1);
                        values.Push(int.Parse(value));
                        break;
                    }
                }
            }

            while (operations.Any())
                ProcessOperation(values, operations.Pop());


            return values.Pop();
        }

        private static void ProcessOperation(Stack<long> values, char operation)
        {
            var right = values.Pop();
            var left = values.Pop();
            var result = operation switch
            {
                '+' => right + left,
                '*' => right * left,
            };
            values.Push(result);
        }

        private static int Priority(char operation)
        {
            return operation switch
            {
                '(' => -1,
                '*' => 1,
                '+' => 2,
            };
        }

        [TestCase(51, "1 + (2 * 3) + (4 * (5 + 6))")]
        [TestCase(46, "2 * 3 + (4 * 5)")]
        [TestCase(1445, "5 + (8 * 3 + 9 + 3 * 4 * 3)")]
        [TestCase(669060, "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))")]
        [TestCase(23340, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2")]
        public void Example(int expected, string expression)
        {
            Assert.AreEqual(expected, Process(expression));
        }
    }
}