using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static bool enabled = true;

    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = rows.Sum(CheckMem);

        Console.WriteLine(sum);
    }

    static long CheckMem(string mem)
    {
        var pattern = @"(mul\([0-9]?[0-9]?[0-9],[0-9]?[0-9]?[0-9]\))|(do\(\))|(don\'t\(\))";

        var matches = Regex.Matches(mem, pattern);

        long sum = 0;

        foreach (var match in matches)
        {
            var s = match.ToString();

            if (s == null)
            {
                continue;
            }

            if (s == "do()")
            {
                enabled = true;
            }
            else if (s == "don't()")
            {
                enabled = false;
            }
            else if (enabled)
            {
                sum += s[4..^1].Split(',').Select(long.Parse).Aggregate((a, b) => a * b);
            }
        }

        return sum;
    }
}
