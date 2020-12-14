using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("AOC 14a");
stopwatch.Start();

var result = new Solver().Solve(args[0]);
stopwatch.Stop();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");


public class Solver
{
    public double Solve(string filename)
    {
        Func<long, string, long> Mask = (value, mask) => 
        {
            string binValue = Convert.ToString(value, 2).PadLeft(mask.Length, '0');
            IEnumerable<char> chars = binValue.Select((c, i) => mask[i] == 'X' ? c : mask[i]);
            string masked = new string(chars.ToArray());
            return Convert.ToInt64(masked, 2);
        };

        var lines = File.ReadAllLines(filename);

        string mask = "";
        string memoryAddress = "";
        long value = 0;

        var assignments = new Dictionary<string, long>(lines.Length);

        foreach (var line in lines)
        {
            if (line.StartsWith("mask"))
            {
                mask = line.Remove(0, "mask = ".Length);
                continue;
            }
            var parts = line.Split('=');
            memoryAddress = parts[0];
            value = Mask(long.Parse(parts[1]), mask);

            assignments[memoryAddress] = value;
        }

        return assignments.Sum(a => a.Value);
    }
}