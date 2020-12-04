using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode1b
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            Console.WriteLine("AOC 1b");
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
            var numbers = lines.Select(l => int.Parse(l)).ToList();
            var sums = new Dictionary<int, Numbers>();


            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (j==i)
                        continue;
                    int sum = numbers[i] + numbers[j];
                    if (!sums.ContainsKey(sum))
                    {
                        sums.Add(sum, new Numbers { Number1 = numbers[i], Number2 = numbers[j] });
                    }
                }
            }

            foreach (int number in numbers)
            {
                int rest = 2020 - number;
                if (sums.ContainsKey(rest))
                    return number * sums[rest].Product();
            }

            return -1;
        }
    }

    public struct Numbers
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }

        public int Product()
        {
            return Number1 * Number2;
        }
    }
}
