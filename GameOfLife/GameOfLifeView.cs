using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameOfLifeView
    {
        public void Start()
        {
            int timeDelay = 105;
            while (true)
            {
                GameOfLife game = new GameOfLife();
                Random random = new Random();

                int rows = 58;
                int cols = 199;

                int initialPopulation = 0;
                bool[,] map = new bool[rows, cols];

                for (int spawnTryCount = 0; spawnTryCount < (rows*cols/5); spawnTryCount++)
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
                
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.OutputEncoding = Encoding.Unicode;

                int minDelay = 15;
                int maxDelay = 500;
                
                int generation = 0;
                int population;
                int priorPopulation = 0;
                bool life = true;

                while (life)
                {
                    Console.CursorVisible = false;
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
                        if(Console.KeyAvailable)
                        {
                            if(ModifyGame(ref timeDelay, ref life))
                            {
                                int thisRow = random.Next(rows);
                                int thisCol = random.Next(cols);
                                map[thisRow, thisCol] = true;
                            }
                        }
                        
                        if (timeDelay < minDelay)
                        {
                            timeDelay = minDelay;
                        }
                        else if (timeDelay > maxDelay)
                        {
                            timeDelay = maxDelay;
                        }
                    }
                    now = DateTime.Now;
                }
            }
        }

        private bool ModifyGame(ref int currentSpeed, ref bool life)
        {
            int offset = 0;
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            if (pressed.Key == ConsoleKey.DownArrow)
            {
                offset = 10;
            }
            if (pressed.Key == ConsoleKey.UpArrow)
            {
                offset = -10;
            }
            currentSpeed += offset;

            if(pressed.Key == ConsoleKey.R)
            {
                life = false;
            }

            if(pressed.Key == ConsoleKey.N)
            {
                return true;
            }
            return false;
        }
    }
}
