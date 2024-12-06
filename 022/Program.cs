using System.IO;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = rows.Where(r => CheckReportAcceptOneBadLevel([.. r.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n))])).Count();

        Console.WriteLine(sum);
    }

    static bool CheckReportAcceptOneBadLevel(int[] levels)
    {
        if (CheckReport(levels))
        {
            return true;
        }

        for (var j = 0; j < levels.Length; j++)
        {
            int[] levels2 = [.. levels.Take(j).Concat(levels.Skip(j + 1))];
            if (CheckReport(levels2))
            {
                return true;
            }
        }

        return false;
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
