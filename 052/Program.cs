using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = CheckProducers(rows);

        Console.WriteLine(sum);
    }

    static int CheckProducers(string[] rows)
    {
        var rules = new List<(int a, int b)>();

        foreach (var row in rows)
        {
            if (row.Contains('|'))
            {
                var values = row.Split('|');

                rules.Add((int.Parse(values[0]), int.Parse(values[1])));
            }
        }

        var updates = new List<List<int>>();

        foreach (var row in rows)
        {
            if (row.Contains(','))
            {
                List<int> values = [.. row.Split(',').Select(int.Parse)];

                updates.Add(values);
            }
        }

        for (var update = 0; update < updates.Count;)
        {
            var valid = true;
            for (var i = 0; i < updates.Count - 1 && valid; i++)
            {
                for (var j = i + 1; j < updates[update].Count && valid; j++)
                {
                    var validPair = CheckOrders(rules, updates[update][i], updates[update][j]);
                    if (!validPair)
                    {
                        valid = false;
                    }
                }
            }
            if (valid)
            {
                updates.RemoveAt(update);
            }
            else
            {
                update++;
            }
        }

        var sum = 0;

        foreach (var update in updates)
        {
            var goon = true;
            while (goon)
            {
                var valid = true;
                for (var i = 0; i < update.Count - 1 && valid; i++)
                {
                    for (var j = i + 1; j < update.Count && valid; j++)
                    {
                        var validPair = CheckOrders(rules, update[i], update[j]);
                        if (!validPair)
                        {
                            (update[j], update[i]) = (update[i], update[j]);
                            valid = false;
                        }
                    }
                }
                if (valid)
                {
                    goon = false;
                }
            }
            sum += update[update.Count / 2];
        }

        return sum;
    }

    static bool CheckOrders(List<(int a, int b)> rules, int a, int b)
    {
        foreach (var rule in rules)
        {
            if (rule.a == a && rule.b == b)
            {
                return true;
            }
            if (rule.a == b && rule.b == a)
            {
                return false;
            }
        }

        return true;
    }
}
