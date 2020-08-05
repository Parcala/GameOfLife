using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameOfLifeView
    {
        public void Start()
        {
            int timeDelay = 150;
            while (true)
            {
                GameOfLife game = new GameOfLife();
                Random random = new Random();
                int rows = 49;
                int cols = 99;
                int initialPopulation = 0;
                bool[,] map = new bool[rows, cols];
                for (int life = 0; life < 750; life++)
                {
                    int thisRow = random.Next(rows);
                    int thisCol = random.Next(cols);
                    if(!map[thisRow, thisCol])
                    {
                        initialPopulation++;
                    }
                    map[thisRow, thisCol] = true;
                }

                DateTime start = DateTime.Now;
                DateTime now = DateTime.Now;

                Console.SetWindowSize(cols + 1, rows + 3);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.OutputEncoding = Encoding.Unicode;

                int minDelay = 10;
                int maxDelay = 500;
                
                int generation = 0;
                int population;
                int priorPopulation = 0;

                while (true)
                {
                    if ((now - start).Milliseconds > timeDelay)
                    {
                        population = 0;
                        //Console.Clear();
                        generation++;
                        start = now;
                        Console.SetCursorPosition(0, 0);
                        for (int row = 0; row < map.GetLength(0); row++)
                        {
                            StringBuilder line = new StringBuilder();
                            for (int col = 0; col < map.GetLength(1); col++)
                            {
                                if (map[row, col])
                                {
                                    population++;
                                    line.Append("O");
                                }
                                else
                                {
                                    line.Append(" ");
                                }
                            }
                            Console.WriteLine(line);
                        }
                        map = game.LifeGenerator(map);
                        Console.WriteLine();
                        Console.Write($"Population: {population,-5}{(double)population/initialPopulation, 5:P} | Population Direction: {(population - priorPopulation == 0 ? "\u2192" : (population - priorPopulation > 0 ? "\u2191" : "\u2193")),-10} | Generation: {generation,-10}");
                        priorPopulation = population;
                    }
                    CheckSpeedAdjustment(ref timeDelay);
                    if (timeDelay < minDelay)
                    {
                        timeDelay = minDelay;
                    }
                    else if (timeDelay > maxDelay)
                    {
                        timeDelay = maxDelay;
                    }
                    now = DateTime.Now;
                    if(CheckRestart())
                    {
                        break;
                    }
                }
            }
        }

        private bool CheckRestart()
        {
            if(Console.KeyAvailable)
            {
                if(Console.ReadKey(true).Key == ConsoleKey.R)
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckSpeedAdjustment(ref int currentSpeed)
        {
            int offset = 0;
            ConsoleKeyInfo pressed = new ConsoleKeyInfo('n', ConsoleKey.N, false, false, false);
            if (Console.KeyAvailable)
            {
                pressed = Console.ReadKey(true);
                if(pressed.Key == ConsoleKey.DownArrow)
                {
                    offset = 10;
                }
                if(pressed.Key == ConsoleKey.UpArrow)
                {
                    offset = -10;
                }
            }
            currentSpeed += offset;
        }
    }
}
