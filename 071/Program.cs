using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = SumSolutions(rows);

        Console.WriteLine(sum);
    }

    static long SumSolutions(string[] rows)
    {
        long sum = 0;

        foreach (var row in rows)
        {
            long[] allnumbers = [.. row.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)];

            var alloperators = GetAllOperatorCombinations(allnumbers.Length - 2);
            foreach (var operators in alloperators)
            {
                var result = CalcExpression([.. allnumbers.Skip(1)], operators);
                if (result == allnumbers[0])
                {
                    sum += allnumbers[0];
                    break;
                }
            }
        }

        return sum;
    }

    static char[][] GetAllOperatorCombinations(int count)
    {
        if (count < 1)
        {
            return [[]];
        }

        List<char[]> alloperators = [];

        var combinations = GetAllOperatorCombinations(count - 1);
        foreach (var combination in combinations)
        {
            alloperators.Add(['+', .. combination]);
            alloperators.Add(['*', .. combination]);
        }

        return [.. alloperators];
    }

    static long CalcExpression(long[] numbers, char[] operators)
    {
        var sum = numbers[0];

        foreach (var (o, n) in operators.Zip(numbers.Skip(1)))
        {
            if (o == '+')
            {
                sum += n;
            }
            else if (o == '*')
            {
                sum *= n;
            }
        }

        return sum;
    }
}
