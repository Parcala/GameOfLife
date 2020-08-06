using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameOfLifeView
    {
        public void Start()
        {
            //set the initial speed, lower is faster
            //105 is a good pace, set to 150 for the purpose of presenting
            int timeDelay = 150;

            while (true)
            {
                GameOfLife game = new GameOfLife();
                Random random = new Random();

                //set the height, then width, now should be set based on the consoles ability to scale
                int rows = Console.LargestWindowHeight - 5; //was 58
                int cols = Console.LargestWindowWidth - 5; //was 199

                //set the window size so that it doesn't cause issues
                Console.SetWindowSize(cols + 1, rows + 3);



                //initialize the map with no life
                int initialPopulation = 0;
                bool[,] map = new bool[rows, cols];

                //randomly seed life, based on the size of the map
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

                //set variable for keeping the pace
                DateTime start = DateTime.Now;
                DateTime now = DateTime.Now;

                //set display output color and type so all symbols display properly
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.OutputEncoding = Encoding.Unicode;

                //used to confine the speed to thesh-holds
                int minDelay = 15;
                int maxDelay = 500;
                
                //used for display and determining if the board should be reset
                int generation = 0;
                int population;
                int priorPopulation = 0;
                bool life = true;

                while (life)
                {
                    //set cursor to invisible here, incase board was resized (larger) this makes it go away again
                    Console.CursorVisible = false;

                    //if enough time has passed update the board
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
                                    line.Append("●");
                                }
                                else
                                {
                                    line.Append(" ");
                                }
                            }
                            Console.WriteLine(line);
                        }

                        // determine the next generation
                        map = game.LifeGenerator(map);

                        //add a line and write the statistics
                        Console.WriteLine();
                        Console.Write($"Population: {population,-5}{(double)population/initialPopulation, 5:P} | Population Direction: {(population - priorPopulation == 0 ? "\u2192" : (population - priorPopulation > 0 ? "\u2191" : "\u2193")),-10} | Generation: {generation,-10}");
                        priorPopulation = population;                        
                    }

                    //check to see if the user wants to make any changes
                    //up arrow increases speed, down arrow slows it down, n adds new life, and r restarts with a new random board
                    if (Console.KeyAvailable)
                    {
                        if (ModifyGame(ref timeDelay, ref life))
                        {
                            int thisRow = random.Next(rows);
                            int thisCol = random.Next(cols);
                            map[thisRow, thisCol] = true;
                        }

                        //make sure the timedelay is within parameters, only needs checked here after it could be modified.
                        if (timeDelay < minDelay)
                        {
                            timeDelay = minDelay;
                        }
                        else if (timeDelay > maxDelay)
                        {
                            timeDelay = maxDelay;
                        }
                    }

                    //set the current time for pacing
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
