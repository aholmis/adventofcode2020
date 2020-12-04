using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2a
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            Console.WriteLine("AOC 2a");
            int result = new Solver().Solve(args[0]);
            Console.WriteLine($"Result: {result}");

            // Stop timing.
            stopwatch.Stop();

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
    }

    public class Solver
    {
        public int Solve(string filename)
        {
            var lines = File.ReadAllLines(filename);
            int correctCount = 0;
            foreach (string line in lines)
            {
                var parts = line.Split(':');

                var policy = new Policy(parts[0].Trim());
                var password = parts[1].Trim();

                int letters = password.Count(c => c == policy.Letter);
                if (letters >= policy.Min && letters <= policy.Max)
                    correctCount++;
            }

            return correctCount;
        }
    }

    struct Policy
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Letter { get; set; }

        public Policy(string content)
        {
            var parts = content.Split(' ');
            Letter = parts[1][0];
            var limits = parts[0].Split('-');
            Min = int.Parse(limits[0]);
            Max = int.Parse(limits[1]);
        }
    }
}
