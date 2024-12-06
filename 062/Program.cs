using System;
using System.ComponentModel;
using System.IO;

class Program
{
    enum Escape
    {
        OnGrid,
        OutOfGrid,
        Loop
    }

    static void Main()
    {
        var rows = File.ReadAllLines("input.txt");

        var sum = SumLoops(rows);

        Console.WriteLine(sum);
    }

    static int SumLoops(string[] rows)
    {
        var sum = 0;

        for (var yy = 0; yy < rows.Length; yy++)
        {
            for (var xx = 0; xx < rows[yy].Length; xx++)
            {
                if (rows[yy][xx] == '.')
                {
                    var grid = InitGrid(rows, out int x, out int y, out int z);

                    grid[xx, yy] = 0x40;

                    if (IsLoopRoute(grid, x, y, z))
                    {
                        //WriteGrid(grid, x, y, z);
                        sum++;
                    }
                }
            }
        }

        return sum;
    }

    static byte[,] InitGrid(string[] rows, out int x, out int y, out int z)
    {
        var grid = new byte[rows[0].Length, rows.Length];

        var w = grid.GetLength(0);
        var h = grid.GetLength(1);

        x = y = z = 0;

        for (var yy = 0; yy < h; yy++)
        {
            for (var xx = 0; xx < w; xx++)
            {
                grid[xx, yy] = rows[yy][xx] switch
                {
                    '^' or '>' or 'v' or '<' or '.' => 0,
                    '#' => 0x80,
                    _ => throw new InvalidEnumArgumentException()
                };

                if (rows[yy][xx] is '^' or '>' or 'v' or '<')
                {
                    x = xx;
                    y = yy;
                    z = rows[yy][xx] switch
                    {
                        '^' => 0,
                        '>' => 1,
                        'v' => 2,
                        '<' => 3,
                        _ => throw new InvalidEnumArgumentException()
                    };
                }
            }
        }

        return grid;
    }

    static bool IsLoopRoute(byte[,] grid, int x, int y, int z)
    {
        var gx = x;
        var gy = y;
        var gz = z;

        var result = Escape.OnGrid;
        while (result == Escape.OnGrid)
        {
            result = MoveOneStep(grid, ref gx, ref gy, ref gz);
            if (result == Escape.OutOfGrid)
            {
                return false;
            }
            if (result == Escape.Loop)
            {
                return true;
            }
        }

        throw new InvalidOperationException();
    }

    /*
    static void WriteGrid(byte[,] grid, int x, int y, int z)
    {
        var w = grid.GetLength(0);
        var h = grid.GetLength(1);

        for (var yy = 0; yy < h; yy++)
        {
            for (var xx = 0; xx < w; xx++)
            {
                if (xx == x && yy == y)
                {
                    Console.Write(z switch
                    {
                        0 => '^',
                        1 => '>',
                        2 => 'v',
                        3 => '<',
                        _ => throw new InvalidEnumArgumentException()
                    });
                }
                else
                {
                    char c;
                    if ((grid[xx, yy] & 0x80) != 0)
                    {
                        c = '#';
                    }
                    else if ((grid[xx, yy] & 0x40) != 0)
                    {
                        c = 'O';
                    }
                    else if ((grid[xx, yy] & 0x0F) != 0)
                    {
                        var vv = ((grid[xx, yy] & 0x01) != 0) || ((grid[xx, yy] & 0x04) != 0);
                        var hh = ((grid[xx, yy] & 0x02) != 0) || ((grid[xx, yy] & 0x08) != 0);
                        if (vv && hh)
                        {
                            c = '+';
                        }
                        else if (vv)
                        {
                            c = '|';
                        }
                        else if (hh)
                        {
                            c = '-';
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    else
                    {
                        c = '.';
                    }
                    Console.Write(c);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    */

    static Escape MoveOneStep(byte[,] grid, ref int x, ref int y, ref int z)
    {
        var w = grid.GetLength(0);
        var h = grid.GetLength(1);

        if (z == 0)
        {
            if ((grid[x, y] & 0x01) != 0)
            {
                return Escape.Loop;
            }
            grid[x, y] = (byte)(grid[x, y] | 0x01);
            if (y == 0)
            {
                return Escape.OutOfGrid;
            }
            if ((grid[x, y - 1] & 0xC0) == 0)
            {
                y--;
            }
            else
            {
                z = 1;
            }
        }
        else if (z == 1)
        {
            if ((grid[x, y] & 0x02) != 0)
            {
                return Escape.Loop;
            }
            grid[x, y] = (byte)(grid[x, y] | 0x02);
            if (x == w - 1)
            {
                return Escape.OutOfGrid;
            }
            if ((grid[x + 1, y] & 0xC0) == 0)
            {
                x++;
            }
            else
            {
                z = 2;
            }
        }
        else if (z == 2)
        {
            if ((grid[x, y] & 0x04) != 0)
            {
                return Escape.Loop;
            }
            grid[x, y] = (byte)(grid[x, y] | 0x04);
            if (y == h - 1)
            {
                return Escape.OutOfGrid;
            }
            if ((grid[x, y + 1] & 0xC0) == 0)
            {
                y++;
            }
            else
            {
                z = 3;
            }
        }
        else if (z == 3)
        {
            if ((grid[x, y] & 0x08) != 0)
            {
                return Escape.Loop;
            }
            grid[x, y] = (byte)(grid[x, y] | 0x08);
            if (x == 0)
            {
                return Escape.OutOfGrid;
            }
            if ((grid[x - 1, y] & 0xC0) == 0)
            {
                x--;
            }
            else
            {
                z = 0;
            }
        }

        return Escape.OnGrid;
    }
}
