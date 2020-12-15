using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

Stopwatch stopwatch = new Stopwatch();
Console.WriteLine("AOC 14b");
stopwatch.Start();

var result = new Solver().Solve(args[0]);
stopwatch.Stop();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");


public class Solver
{
    private readonly Func<long, string, string, long> Mask = (value, mask, replace) =>
        {
            string binValue = Convert.ToString(value, 2).PadLeft(mask.Length, '0');
            int j = 0;
            IEnumerable<char> chars = binValue.Select((c, i) => mask[i] == '0' ? c : mask[i] == 'X' ? replace[j++]  : '1');
            string masked = new string(chars.ToArray());
            return Convert.ToInt64(masked, 2);
        };

    public long Solve(string filename)
    {
        var lines = File.ReadAllLines(filename);

        string mask = "";

        var assignments = new Dictionary<long, long>(lines.Length*10);

        foreach (var line in lines)
        {
            if (line.StartsWith("mask"))
            {
                mask = line.Remove(0, "mask = ".Length);
                continue;
            }
            var startIndex = line.IndexOf('[');
            var endIndex = line.IndexOf(']');
            var memoryAddress = long.Parse(line.Substring(startIndex + 1, endIndex - startIndex - 1));

            int xCount = mask.Count(c => c == 'X');
            var numberOfMemAssignments = Math.Pow(2, xCount);
            for (int i = 0; i < numberOfMemAssignments; i++)
            {
                string replaceX = Convert.ToString(i, 2).PadLeft(xCount, '0');
                long maskedMemAdr = Mask(memoryAddress, mask, replaceX);
                assignments[maskedMemAdr] = long.Parse(line.Split("=")[1]);
            }
        }

        return assignments.Sum(a => a.Value);
    }
}