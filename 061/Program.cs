using System;
using System.IO;

class Program
{
    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = SumRoute(rows);

        Console.WriteLine(sum);
    }

    static int SumRoute(string[] rows)
    {
        var grid = new char[rows[0].Length, rows.Length];

        for (var yy = 0; yy < rows.Length; yy++)
        {
            for (var xx = 0; xx < rows[yy].Length; xx++)
            {
                grid[xx, yy] = rows[yy][xx];
            }
        }

        WhereIsTheGuard(grid, out int x, out int y);

        var ongrid = true;
        while (ongrid)
        {
            ongrid = MoveOneStep(grid, ref x, ref y);
        }

        var sum = 0;

        for (var yy = 0; yy < rows.Length; yy++)
        {
            for (var xx = 0; xx < rows[yy].Length; xx++)
            {
                if (grid[xx, yy] == 'X')
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    static void WhereIsTheGuard(char[,] grid, out int x, out int y)
    {
        var w = grid.GetLength(0);
        var h = grid.GetLength(1);

        x = y = 0;

        for (var yy = 0; yy < h; yy++)
        {
            for (var xx = 0; xx < w; xx++)
            {
                if (grid[xx, yy] is '^' or '>' or '<' or 'v')
                {
                    x = xx;
                    y = yy;
                }
            }
        }
    }

    static bool MoveOneStep(char[,] grid, ref int x, ref int y)
    {
        var w = grid.GetLength(0);
        var h = grid.GetLength(1);

        if (grid[x, y] == '^')
        {
            grid[x, y] = 'X';
            if (y == 0)
            {
                return false;
            }
            if (grid[x, y - 1] != '#')
            {
                grid[x, y - 1] = '^';
                y--;
            }
            else
            {
                if (x == w - 1)
                {
                    return false;
                }
                grid[x + 1, y] = '>';
                x++;
            }
        }
        else if (grid[x, y] == '>')
        {
            grid[x, y] = 'X';
            if (x == w - 1)
            {
                return false;
            }
            if (grid[x + 1, y] != '#')
            {
                grid[x + 1, y] = '>';
                x++;
            }
            else
            {
                if (y == h - 1)
                {
                    return false;
                }
                grid[x, y + 1] = 'v';
                y++;
            }
        }
        else if (grid[x, y] == 'v')
        {
            grid[x, y] = 'X';
            if (y == h - 1)
            {
                return false;
            }
            if (grid[x, y + 1] != '#')
            {
                grid[x, y + 1] = 'v';
                y++;
            }
            else
            {
                if (x == 0)
                {
                    return false;
                }
                grid[x - 1, y] = '<';
                x--;
            }
        }
        else if (grid[x, y] == '<')
        {
            grid[x, y] = 'X';
            if (x == 0)
            {
                return false;
            }
            if (grid[x - 1, y] != '#')
            {
                grid[x - 1, y] = '<';
                x--;
            }
            else
            {
                if (y == 0)
                {
                    return false;
                }
                grid[x, y - 1] = '^';
                y--;
            }
        }

        return true;
    }
}
