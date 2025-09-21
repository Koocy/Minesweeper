using System;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

class Program
{
    class Box
    {
        public bool isBomb = false, isOpen = false, isFlag = false;
        public int nearby = 0;
    }

    static Box[,] grid = new Box[10, 10];
    static int nBomb = 16;
    static bool gameOver = false, victory = false;
    static int imlecx = 0, imlecy = 0;
    static int rFlag = nBomb, sFlag = 0;

    public static void assignBombs()
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                grid[j, i] = new Box();

        int bombalar = 0;
        Random random = new Random();

        while (bombalar < nBomb)
        {
            int rnd1 = random.Next(0, 10);
            Thread.Sleep(20);
            int rnd2 = random.Next(0, 10);

            if (!grid[rnd1, rnd2].isBomb)
            {
                grid[rnd1, rnd2].isBomb = true;

                if (rnd1 > 0)
                {
                    grid[rnd1 - 1, rnd2].nearby++;

                    if (rnd2 > 0)
                    {
                        grid[rnd1 - 1, rnd2 - 1].nearby++;
                    }

                    if (rnd2 < 9)
                    {
                        grid[rnd1 - 1, rnd2 + 1].nearby++;
                    }
                }

                if (rnd1 < 9)
                {
                    grid[rnd1 + 1, rnd2].nearby++;


                    if (rnd2 > 0)
                    {
                        grid[rnd1 + 1, rnd2 - 1].nearby++;
                    }

                    if (rnd2 < 9)
                    {
                        grid[rnd1 + 1, rnd2 + 1].nearby++;
                    }
                }

                if (rnd2 > 0)
                {
                    grid[rnd1, rnd2 - 1].nearby++;
                }

                if (rnd2 < 9)
                {
                    grid[rnd1, rnd2 + 1].nearby++;
                }

                bombalar++;
            }

            Thread.Sleep(20);
        }
    }

    public static void render()
    {
        Console.Clear();

        Console.Write("   ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i.ToString() + " ");
        }

        Console.WriteLine("\t\tRemaining flag: " + rFlag + "\tPress 'B' for flag, Enter for opening square");

        for (int y = 0; y < 10; y++)
        {
            Console.Write(y.ToString() + "  ");

            for (int x = 0; x < 10; x++)
            {
                if (x == imlecx && y == imlecy)
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                else Console.BackgroundColor = ConsoleColor.Black;

                if (grid[x, y].isOpen && grid[x, y].isFlag == false)
                {
                    if (grid[x, y].isBomb)
                    {
                        Console.Write("x");
                    }

                    else
                    {
                        Console.Write(grid[x, y].nearby.ToString());
                    }
                }

                else if (grid[x, y].isFlag == false && grid[x, y].isOpen == false)
                {
                    Console.Write("#");
                }

                else if (grid[x, y].isFlag == true && grid[x, y].isOpen == true)
                {
                    Console.Write("@");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public static void etrafiacik(int x, int y)
    {

        if (x > 0)
        {
            if (grid[x - 1, y].nearby == 0 && grid[x - 1, y].isBomb == false && grid[x - 1, y].isOpen == false && grid[x - 1, y].isFlag == false)
            {
                grid[x - 1, y].isOpen = true;
                etrafiacik(x - 1, y);
            }
            else
            {
                grid[x - 1, y].isOpen = true;
            }

            if (y > 0)
            {
                if (grid[x - 1, y - 1].nearby == 0 && grid[x - 1, y - 1].isBomb == false && grid[x - 1, y - 1].isOpen == false && grid[x - 1, y - 1].isFlag == false)
                {
                    grid[x - 1, y - 1].isOpen = true;
                    etrafiacik(x - 1, y - 1);
                }
                else
                {
                    grid[x - 1, y - 1].isOpen = true;
                }
            }

            if (y < 9)
            {
                if (grid[x - 1, y + 1].nearby == 0 && grid[x - 1, y + 1].isBomb == false && grid[x - 1, y + 1].isOpen == false && grid[x - 1, y + 1].isFlag == false)
                {
                    grid[x - 1, y + 1].isOpen = true;
                    etrafiacik(x - 1, y + 1);
                }
                else
                {
                    grid[x - 1, y + 1].isOpen = true;
                }
            }
        }

        if (x < 9)
        {
            if (grid[x + 1, y].nearby == 0 && grid[x + 1, y].isBomb == false && grid[x + 1, y].isOpen == false && grid[x + 1, y].isFlag == false)
            {
                grid[x + 1, y].isOpen = true;
                etrafiacik(x + 1, y);
            }
            else
            {
                grid[x + 1, y].isOpen = true;
            }

            if (y > 0)
            {
                if (grid[x + 1, y - 1].nearby == 0 && grid[x + 1, y - 1].isBomb == false && grid[x + 1, y - 1].isOpen == false && grid[x + 1, y - 1].isFlag == false)
                {
                    grid[x + 1, y - 1].isOpen = true;
                    etrafiacik(x + 1, y - 1);
                }
                else
                {
                    grid[x + 1, y - 1].isOpen = true;
                }
            }

            if (y < 9)
            {
                if (grid[x + 1, y + 1].nearby == 0 && grid[x + 1, y + 1].isBomb == false && grid[x + 1, y + 1].isOpen == false && grid[x + 1, y + 1].isFlag == false)
                {
                    grid[x + 1, y + 1].isOpen = true;
                    etrafiacik(x + 1, y + 1);
                }
                else
                {
                    grid[x + 1, y + 1].isOpen = true;
                }
            }
        }

        if (y > 0)
        {
            if (grid[x, y - 1].nearby == 0 && grid[x, y - 1].isBomb == false && grid[x, y - 1].isOpen == false && grid[x, y - 1].isFlag == false)
            {
                grid[x, y - 1].isOpen = true;
                etrafiacik(x, y - 1);
            }
            else
            {
                grid[x, y - 1].isOpen = true;
            }
        }

        if (y < 9)
        {
            if (grid[x, y + 1].nearby == 0 && grid[x, y + 1].isBomb == false && grid[x, y + 1].isOpen == false && grid[x, y + 1].isFlag == false)
            {
                grid[x, y + 1].isOpen = true;
                etrafiacik(x, y + 1);
            }
            else
            {
                grid[x, y + 1].isOpen = true;
            }
        }
    }

    public static void Game()
    {
        if (gameOver)
        {
            render();

            if (victory) Console.WriteLine("\nCongratulations. You marked all bombs and won!");
            else Console.WriteLine("\nSorry. You lost.");

            Console.WriteLine("Press 'R' to restart...");
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.R)
            {
                gameOver = false;
                victory = false;
                rFlag = nBomb;
                sFlag = 0;
                assignBombs();
                cki = default(ConsoleKeyInfo);
                Game();
            }
            else Game();
        }

        int X = 0;
        int Y = 0;

        ConsoleKeyInfo pressed;

    label:
        pressed = default(ConsoleKeyInfo);
        render();
        Console.WriteLine();

        pressed = Console.ReadKey();

        switch (pressed.Key)
        {
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                if (imlecx > 0)
                    imlecx--;
                goto label;

            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                if (imlecx < 9)
                    imlecx++;
                goto label;

            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                if (imlecy < 9)
                    imlecy++;
                goto label;

            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                if (imlecy > 0)
                    imlecy--;
                goto label;

            case ConsoleKey.Enter:
                if (grid[imlecx, imlecy].isFlag)
                    goto label;
                X = imlecx;
                Y = imlecy;
                break;

            case ConsoleKey.B:

                if (grid[imlecx, imlecy].isFlag == false && grid[imlecx, imlecy].isOpen == false && rFlag > 0)
                {
                    grid[imlecx, imlecy].isFlag = true;
                    grid[imlecx, imlecy].isOpen = true;

                    rFlag--;

                    if (grid[imlecx, imlecy].isBomb)
                        sFlag++;
                }
                else if (grid[imlecx, imlecy].isFlag)
                {
                    grid[imlecx, imlecy].isFlag = false;
                    grid[imlecx, imlecy].isOpen = false;
                    rFlag++;

                    if (grid[imlecx, imlecy].isBomb)
                        sFlag--;
                }

                if (sFlag == nBomb)
                {
                    gameOver = true;
                    victory = true;

                    Game();
                }

                goto label;

            default:
                goto label;
        }

        Box currentBox = grid[X, Y];

        if (currentBox.isBomb)
        {
            gameOver = true;
            currentBox.isOpen = true;
            victory = false;
        }

        else
        {
            if (currentBox.nearby > 0)
            {
                currentBox.isOpen = true;
            }

            else
            {
                currentBox.isOpen = true;
                etrafiacik(X, Y);
            }
        }

        Game();
    }

    static void Main()
    {
        assignBombs();
        Game();
        Console.ReadKey();
    }
}

