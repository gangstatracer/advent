using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day16 : TestBase
    {
        [Test]
        public void Task()
        {
            var input = ReadAll().Split(Environment.NewLine + Environment.NewLine).ToArray();
            var rules = input[0]
                .Split(Environment.NewLine)
                .Select(l =>
                {
                    var (field, value) = Slice(l, ": ");
                    var ranges = value
                        .Split(" or ")
                        .Select(r =>
                        {
                            var (left, right) = Slice(r, "-");
                            return (left: int.Parse(left), right: int.Parse(right));
                        })
                        .ToArray();
                    return (field, ranges);
                })
                .ToArray();

            var myTicket = input[1]
                .Split(Environment.NewLine)
                .Last()
                .Split(",")
                .Select(int.Parse)
                .ToArray();

            var tickets = input[2]
                .Split(Environment.NewLine)
                .Skip(1)
                .Select(t => t.Split(",").Select(int.Parse).ToArray())
                .ToArray();

            Console.WriteLine(Task1(tickets, rules));

            var validTickets =
                tickets.Where(t => t.All(i => rules.Any(rule => rule.ranges.Any(r => RangeContains(r, i)))));


            var sudoku = Candidates(rules, myTicket);
            foreach (var ticket in validTickets)
            {
                var candidates = Candidates(rules, ticket);
                for (var i = 0; i < sudoku.Length; i++)
                    sudoku[i].IntersectWith(candidates[i]);
            }

            var crossed = new HashSet<string>();
            while (sudoku.Any(c => c.Count != 1))
            {
                var single = sudoku
                    .First(c => c.Count == 1 && !crossed.Contains(c.First()))
                    .First();

                foreach (var candidates in sudoku)
                {
                    if (candidates.Count > 1)
                        candidates.Remove(single);
                }

                crossed.Add(single);
            }

            var task2 = sudoku.Select((c, i) => (c, i))
                .Where(t => t.c.First().StartsWith("departure")).Aggregate(1L, (r, t) => r * myTicket[t.i]);
            Console.WriteLine(task2);
        }

        private static HashSet<string>[] Candidates(
            (string field, (int left, int right)[] ranges)[] rules,
            IEnumerable<int> ticket)
        {
            return ticket
                .Select(t => rules
                    .Where(rule => rule.ranges.Any(r => RangeContains(r, t)))
                    .Select(rule => rule.field)
                    .ToHashSet())
                .ToArray();
        }

        private static int Task1(int[][] tickets, (string, (int left, int right)[] ranges)[] rules)
        {
            return tickets
                .SelectMany(t => t)
                .Where(i => rules.All(rule => rule.ranges.All(r => !RangeContains(r, i))))
                .Sum();
        }

        private static bool RangeContains((int Left, int Right) range, int i)
        {
            return i >= range.Left && i <= range.Right;
        }
    }
}