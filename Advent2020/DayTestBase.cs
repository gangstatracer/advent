using System;
using System.Collections.Generic;
using System.IO;

namespace Advent2020
{
    public class DayTestBase
    {
        protected IEnumerable<string> ReadLines()
        {
            return File.ReadAllLines(GetFile());
        }

        protected string ReadAll()
        {
            return File.ReadAllText(GetFile());
        }

        private string GetFile()
        {
            return Path.Combine("Inputs", "input" + $"{GetType().Name.Substring(3)}" + ".txt");
        }

        protected static (string, string) Slice(string value, string separator = " ")
        {
            var parts = value.Split(separator);
            if (parts.Length != 2)
                throw new Exception();
            return (parts[0], parts[1]);
        }
    }
}