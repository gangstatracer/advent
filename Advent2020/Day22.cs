using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Advent2020
{
    public class Day22 : DayTestBase
    {
        [Test]
        public void Task()
        {
            var decks = ReadAll()
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(p => new LinkedList<int>(p.Split(Environment.NewLine).Skip(1).Select(int.Parse)))
                .ToArray();

            Console.WriteLine(Score(CrabCombat(decks)));
        }

        [Test]
        public void Task2()
        {
            var decks = ReadAll()
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(p => new LinkedList<int>(p.Split(Environment.NewLine).Skip(1).Select(int.Parse)))
                .ToArray();

            var winner = RecursiveCombat(decks);
            Console.WriteLine(Score(decks[winner]));
        }

        [Test]
        public void Task2Example()
        {
            var decks = new[]
            {
                new LinkedList<int>(new[] {9, 2, 6, 3, 1}),
                new LinkedList<int>(new[] {5, 8, 4, 7, 10}),
            };
            var winner = RecursiveCombat(decks);
            Assert.AreEqual(1, winner);
            Assert.AreEqual(291, Score(decks[winner]));
        }


        private static long Score(IReadOnlyCollection<int> deck)
        {
            return deck.Select((c, i) => (long) c * (deck.Count - i)).Sum();
        }

        private static int RecursiveCombat(IReadOnlyList<LinkedList<int>> decks)
        {
            var roundsHashes = new HashSet<string>();
            while (decks.Count(d => d.Any()) > 1 && !roundsHashes.Contains(GetHash(decks)))
            {
                roundsHashes.Add(GetHash(decks));
                var turn = new int[decks.Count];
                var maxValue = 0;
                var maxIndex = 0;
                var startRecursion = true;
                for (var i = 0; i < decks.Count; i++)
                {
                    if (decks[i].Any())
                    {
                        var card = decks[i].First!.Value;
                        turn[i] = card;
                        decks[i].RemoveFirst();
                        if (card > maxValue)
                        {
                            maxIndex = i;
                            maxValue = card;
                        }

                        if (decks[i].Count < card)
                            startRecursion = false;
                    }
                }

                if (startRecursion)
                    maxIndex = RecursiveCombat(decks
                        .Select((t, i) => new LinkedList<int>(t.Take(turn[i])))
                        .ToArray());


                decks[maxIndex].AddLast(turn[maxIndex]);
                for (var i = 0; i < turn.Length; i++)
                {
                    if (i != maxIndex && turn[i] != 0)
                        decks[maxIndex].AddLast(turn[i]);
                }
            }

            for (var i = 0; i < decks.Count; i++)
                if (decks[i].Any())
                    return i;

            throw new Exception();
        }

        private static string GetHash(IEnumerable<LinkedList<int>> decks)
        {
            return string.Join(";", decks.Select(d => string.Join(",", d)));
        }

        private static LinkedList<int> CrabCombat(IReadOnlyList<LinkedList<int>> decks)
        {
            while (decks.Count(d => d.Any()) > 1)
            {
                var turn = new List<int>();
                var maxValue = 0;
                var maxIndex = 0;
                for (var i = 0; i < decks.Count; i++)
                {
                    if (decks[i].Any())
                    {
                        var card = decks[i].First!.Value;
                        turn.Add(card);
                        decks[i].RemoveFirst();
                        if (card > maxValue)
                        {
                            maxIndex = i;
                            maxValue = card;
                        }
                    }
                }

                foreach (var card in turn.Where(c => c > 0).OrderByDescending(c => c))
                {
                    decks[maxIndex].AddLast(card);
                }
            }

            var winner = decks.Single(p => p.Any());
            return winner;
        }
    }
}