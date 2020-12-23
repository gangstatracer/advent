using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day23 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var labels = "156794823".Select(c => c - '0').ToArray();
            var one = Play(labels, 100);
            Console.WriteLine(Stringify(one));
        }

        [Test]
        public void Task2()
        {
            var labels = "156794823".Select(c => c - '0').Concat(Enumerable.Range(10, 1_000_000 - 9)).ToArray();
            var one = Play(labels, 10_000_000);
            Console.WriteLine((long)one.Next.Value * one.Next.Next.Value);
        }

        [TestCase("192658374", 10)]
        [TestCase("167384529", 100)]
        public void Example(string expected, int moves)
        {
            var labels = new[] {3, 8, 9, 1, 2, 5, 4, 6, 7};
            Assert.AreEqual(expected, Stringify(Play(labels, moves)));
        }

        private static Node Play(int[] labels, int moves)
        {
            var maxValue = labels.Max();
            var minValue = labels.Min();
            var (first, links) = BuildCircle(labels);

            var current = first;
            for (var i = 0; i < moves; i++)
            {
                var cutNodes = new HashSet<int>();
                var cutStart = current.Next;
                var cutEnd = cutStart;
                for (var j = 0; j < 2; j++)
                {
                    cutNodes.Add(cutEnd.Value);
                    cutEnd = cutEnd.Next;
                }

                cutNodes.Add(cutEnd.Value);
                current.Next = cutEnd.Next;

                var desiredLabel = current.Value;
                do
                {
                    desiredLabel--;
                    if (desiredLabel < minValue)
                        desiredLabel = maxValue;
                } while (cutNodes.Contains(desiredLabel));

                var destination = links[desiredLabel];
                cutEnd.Next = destination.Next;
                for (var j = 0; j < 2; j++)
                {
                    destination.Next = cutStart;
                    destination = destination.Next;
                    cutStart = cutStart.Next;
                }

                current = current.Next;
            }

            return links[1];
        }

        private static string Stringify(Node start)
        {
            var current = start;
            var result = "";
            do
            {
                result += current.Value;
                current = current.Next;
            } while (current != start);

            return result;
        }

        private static Node? Find(Node someNode, int searchValue)
        {
            var current = someNode;
            do
            {
                if (current.Value == searchValue)
                    return current;
                current = current.Next;
            } while (current != someNode);

            return null;
        }

        private static (Node Start, Dictionary<int, Node> Links) BuildCircle(IEnumerable<int> labels)
        {
            Node? first = null, last = null;
            var links = new Dictionary<int, Node>();
            foreach (var label in labels.Reverse())
            {
                var previous = new Node {Value = label};
                links.Add(label, previous);
                if (first == null)
                    last = previous;
                previous.Next = first!;
                first = previous;
            }

            last!.Next = first!;
            return (first!, links);
        }

        private class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }

            public override string ToString()
            {
                return $"{Value} => {Next?.Value}";
            }
        }
    }
}