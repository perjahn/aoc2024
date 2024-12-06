using System;
using System.IO;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var word = "XMAS";
        char[] arr = word.ToCharArray();
        Array.Reverse(arr);
        var reverse = new string(arr);

        var sum = 0;
        sum += CheckWordHorizontal(rows, word);
        sum += CheckWordHorizontal(rows, reverse);

        sum += CheckWordVertical(rows, word);
        sum += CheckWordVertical(rows, reverse);

        sum += CheckWordDiagonal1(rows, word);
        sum += CheckWordDiagonal1(rows, reverse);

        sum += CheckWordDiagonal2(rows, word);
        sum += CheckWordDiagonal2(rows, reverse);

        Console.WriteLine(sum);
    }

    static int CheckWordHorizontal(string[] rows, string word)
    {
        var sum = 0;

        foreach (var row in rows)
        {
            for (var i = 0; i <= row.Length - word.Length; i++)
            {
                if (word == row.Substring(i, word.Length))
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    static int CheckWordVertical(string[] rows, string word)
    {
        var sum = 0;

        for (var i = 0; i < rows[0].Length; i++)
        {
            for (var j = 0; j <= rows.Length - word.Length; j++)
            {
                var match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[j + k][i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    static int CheckWordDiagonal1(string[] rows, string word)
    {
        var sum = 0;

        for (var i = 0; i <= rows[0].Length - word.Length; i++)
        {
            for (var j = 0; j <= rows.Length - word.Length; j++)
            {
                var match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[j + k][i + k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    static int CheckWordDiagonal2(string[] rows, string word)
    {
        var sum = 0;

        for (var i = 0; i <= rows[0].Length - word.Length; i++)
        {
            for (var j = 0; j <= rows.Length - word.Length; j++)
            {
                var match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[j + k][i + word.Length - 1 - k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }
            }
        }

        return sum;
    }
}
