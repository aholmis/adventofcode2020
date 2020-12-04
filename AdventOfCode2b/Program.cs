using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2b
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("AOC 2b");
            stopwatch.Start();

            int result = new Solver().Solve(args[0]);
            stopwatch.Stop();

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
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

                bool first = password[policy.Pos1-1] == policy.Letter;
                bool second = password[policy.Pos2-1] == policy.Letter;
                if (first ^ second)
                    correctCount++;
            }

            return correctCount;
        }
    }

    struct Policy
    {
        public int Pos1 { get; set; }
        public int Pos2 { get; set; }
        public char Letter { get; set; }

        public Policy(string content)
        {
            var parts = content.Split(' ');
            Letter = parts[1][0];
            var limits = parts[0].Split('-');
            Pos1 = int.Parse(limits[0]);
            Pos2 = int.Parse(limits[1]);
        }
    }
}
