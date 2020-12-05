using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("AOC 3b");
stopwatch.Start();

var result = new Solver().Solve(args[0]);
stopwatch.Stop();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");


public class Solver
{
    public double Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);

        Func<int, int, double> countTrees = (int dx, int dy) =>
          {
              int width = lines[0].Length;
              double treeCount = 0;
              int x = 0;
              for (int y = 0; y < lines.Length; y += dy)
              {
                  if (x > width - 1)
                      x = x - width;
                  if (lines[y][x] == '#')
                      treeCount++;
                  x += dx;
              }

              return treeCount;
          };

        IEnumerable<(int dx, int dy)> moves = new List<(int dx, int dy)>
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2)
        };

        return moves.Select(move => countTrees(move.dx, move.dy)).Aggregate((count, product) => count * product);
    }
}