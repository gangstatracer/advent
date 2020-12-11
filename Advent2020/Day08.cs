using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day08 : TestBase
    {
        [Test]
        public void Task()
        {
            var lines = ReadLines()
                .Select(l =>
                {
                    var (operation, arg) = Slice(l);
                    var argument = int.Parse(arg);
                    return (Operation: operation, Argument: argument);
                })
                .ToArray();

            for (var i = 0; i < lines.Length; i++)
            {
                lines[i] = SwitchOperation(lines[i]);

                var accumulator = 0;
                var index = 0;
                if (TryReachBreakpoint(lines, ref index, ref accumulator))
                {
                    Console.WriteLine(accumulator);
                    return;
                }

                lines[i] = SwitchOperation(lines[i]);
            }
        }

        private static (string Operation, int Argument) SwitchOperation((string Operation, int Argument) line)
        {
            var (operation, argument) = line;
            operation = operation switch
            {
                "jmp" => "nop",
                "nop" => "jmp",
                _ => operation,
            };
            return (operation, argument);
        }

        private bool TryReachBreakpoint((string Operation, int Argument)[] lines, ref int index,
            ref int accumulator)
        {
            var visitedIndexes = new HashSet<int>();
            while (!visitedIndexes.Contains(index))
            {
                visitedIndexes.Add(index);
                var (operation, argument) = lines[index];
                switch (operation)
                {
                    case "acc":
                        accumulator += argument;
                        index++;
                        break;
                    case "jmp":
                        index += argument;
                        break;
                    case "nop":
                        index++;
                        break;
                    default:
                        throw new Exception();
                }

                if (index >= lines.Length)
                    return true;
            }

            return false;
        }
    }
}