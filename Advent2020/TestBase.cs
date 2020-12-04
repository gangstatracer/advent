using System.Collections.Generic;
using System.IO;

namespace Advent2020
{
    public class TestBase
    {
        protected IEnumerable<string> ReadLines()
        {
            return File.ReadAllLines(Path.Combine("Inputs", "input" + $"{GetType().Name.Substring(3)}" + ".txt"));
        }
    }
}