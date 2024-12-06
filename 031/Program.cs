using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = rows.Sum(CheckMem);

        Console.WriteLine(sum);
    }


    static long CheckMem(string mem)
    {
        var pattern = @"mul\([0-9]?[0-9]?[0-9],[0-9]?[0-9]?[0-9]\)";

        var matches = Regex.Matches(mem, pattern);

        var sum = matches.Sum(m => m.ToString()[4..^1].Split(',').Select(long.Parse).Aggregate((a, b) => a * b));

        return sum;
    }
}
