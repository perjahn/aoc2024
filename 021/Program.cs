using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = rows.Where(r => CheckReport([.. r.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n))])).Count();

        Console.WriteLine(sum);
    }

    static bool CheckReport(int[] levels)
    {
        for (var i = 1; i < levels.Length; i++)
        {
            if (levels[0] < levels[1])
            {
                if (levels[i] - levels[i - 1] is < 1 or > 3)
                {
                    return false;
                }
            }
            else
            {
                if (levels[i - 1] - levels[i] is < 1 or > 3)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
