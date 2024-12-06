using System;
using System.IO;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var word = "MAS";
        var sum = CheckWordDiagonal(rows, word);

        Console.WriteLine(sum);
    }

    static int CheckWordDiagonal(string[] rows, string word)
    {
        var sum = 0;

        for (var i = 0; i <= rows[0].Length - word.Length; i++)
        {
            for (var j = 0; j <= rows.Length - word.Length; j++)
            {
                var match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + k][j + k])
                    {
                        match = false;
                        break;
                    }
                }
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + word.Length - 1 - k][j + k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }

                match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + k][j + k])
                    {
                        match = false;
                        break;
                    }
                }
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + k][j + word.Length - 1 - k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }

                match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + word.Length - 1 - k][j + word.Length - 1 - k])
                    {
                        match = false;
                        break;
                    }
                }
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + word.Length - 1 - k][j + k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    sum++;
                }

                match = true;
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + word.Length - 1 - k][j + word.Length - 1 - k])
                    {
                        match = false;
                        break;
                    }
                }
                for (var k = 0; k < word.Length; k++)
                {
                    if (word[k] != rows[i + k][j + word.Length - 1 - k])
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
