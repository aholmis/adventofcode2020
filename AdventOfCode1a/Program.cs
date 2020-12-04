using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode1a
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

           

            Console.WriteLine("AOC 1a");
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
            Dictionary<int, int> rests = new Dictionary<int, int>();

            foreach (var item in lines)
            {
                int number = int.Parse(item);
                int rest = 2020 - number;
                if (rests.ContainsKey(rest))
                    return number * rest;
                rests.Add(number, 0);
            }
            return -1;
        }
    }
}
