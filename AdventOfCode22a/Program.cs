using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("AOC 22a");
stopwatch.Start();

var result = new Solver().Solve(args[0]);
stopwatch.Stop();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");


public class Solver
{
    public long Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var game = new Game();
        game.DealCards(lines);
        List<int> winnerCards = game.Play();
        
        var count = winnerCards.Count;
        winnerCards.ForEach(c => Console.WriteLine("Card " + c));
        long sum = winnerCards.Select((n, i) => n * (count - i)).Sum();
        return sum;
    }
}

public class Game
{
    private readonly List<Queue<int>> _hands;
    private int _totalCards;

    public Game()
    {
        _hands = new List<Queue<int>>();
    }

    public void DealCards(string[] lines)
    {
        int playerNumber = -1;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            if (line.StartsWith("Player"))
            {
                playerNumber++;
                continue;
            }
            AddCard(playerNumber, int.Parse(line));
        }
    }

    private void AddCard(in int playerNumber, int cardValue)
    {
        if (_hands.Count <= playerNumber)
        {
            _hands.Add(new Queue<int>());
        }
        _hands[playerNumber].Enqueue(cardValue);
        _totalCards++;
    }

    public List<int> Play()
    {
        int playerCount = _hands.Count;
        var table = new int[playerCount];

        do
        {
            int i = 0;
            foreach (var hand in _hands)
            {
                table[i++] = hand.Dequeue();
            }

            int highestValue = 0;
            int leaderPlayer = 0;
            for (int j = 0; j < table.Length; j++)
            {
                if (table[j] > highestValue)
                {
                    highestValue = table[j];
                    leaderPlayer = j;
                }
            }
            foreach (var card in table.OrderByDescending(c => c))
            {
                _hands[leaderPlayer].Enqueue(card);
            }

        } while (_hands.All(h => h.Count != _totalCards));

        return _hands.First(h => h.Count == _totalCards).ToList();
    }

}