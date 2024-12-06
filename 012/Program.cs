using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        List<int> col1numbers = [];
        List<int> col2numbers = [];

        foreach (var row in rows)
        {
            var cols = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            col1numbers.Add(int.Parse(cols[0]));
            col2numbers.Add(int.Parse(cols[1]));
        }

        col1numbers = [.. col1numbers.OrderBy(n => n)];
        col2numbers = [.. col2numbers.OrderBy(n => n)];

        var sum = col1numbers.Sum(n1 => n1 * col2numbers.Count(n2 => n1 == n2));

        Console.WriteLine(sum);
    }
}
