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
            
            var winner1 = CrabCombat(decks);
            Console.WriteLine(Score(winner1));
        }

        private static long Score(IReadOnlyCollection<int> deck)
        {
            return deck.Select((c, i) => (long)c * (deck.Count - i)).Sum();
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