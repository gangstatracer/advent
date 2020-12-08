using System;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day8 : TestBase
    {
        [Test]
        public void Task()
        {
            var lines = ReadLines()
                .Select(l => (Instruction: l, Visited: false))
                .ToArray();

            var accumulator = 0;
            var index = 0;
            TryReachBreakpoint(lines, ref index, ref accumulator);
            Console.WriteLine(accumulator);
        }

        private bool TryReachBreakpoint((string Instruction, bool Visited)[] lines, ref int index, ref int accumulator)
        {
            while (!lines[index].Visited)
            {
                lines[index].Visited = true;
                var (operation, arg) = Slice(lines[index].Instruction);
                var argument = int.Parse(arg);
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