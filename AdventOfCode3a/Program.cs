using System;
using System.Diagnostics;
using System.IO;


Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("AOC 3a");
stopwatch.Start();

int result = new Solver().Solve(args[0]);
stopwatch.Stop();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");


public class Solver
{
    public int Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);
        int width = lines[0].Length;
        int treeCount = 0;
        int x = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            if (x > width - 1)
            {
                x = x - width;
            }
            if (lines[y][x] == '#')
            {
                treeCount++;
            }
            x += 3;
        }

        return treeCount;
    }
}