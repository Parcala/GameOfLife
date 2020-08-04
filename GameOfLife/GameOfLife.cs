using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameOfLife
    {
        public bool[,] LifeGenerator(bool[,] start)
        {
            bool[,] newGrid = new bool[start.GetLength(0), start.GetLength(1)];

            for (int row = 0; row < start.GetLength(0); row++)
            {
                for (int col = 0; col < start.GetLength(1); col++)
                {
                    newGrid[row, col] = start[row, col];
                }
            }


            for (int row = 0; row < start.GetLength(0); row++)
            {
                for (int col = 0; col < start.GetLength(1); col++)
                {
                    int neighbors = NeighborCount(start, row, col);
                    if(neighbors < 2 || neighbors > 3)
                    {
                        newGrid[row, col] = false;
                    }
                    if(!start[row, col] && neighbors == 3)
                    {
                        newGrid[row, col] = true;
                    }
                }
            }
            return newGrid;
        }

        private int NeighborCount(bool[,] grid, int row, int col)
        {
            int count = 0;
            //row above
            if(!((row - 1) < 0) && !((col - 1) < 0) && grid[row-1, col-1])
            {
                count++;
            }
            if (!((row - 1) < 0) && grid[row - 1, col])
            {
                count++;
            }
            if (!((row - 1) < 0) && !((col + 1) == grid.GetLength(1)) && grid[row - 1, col + 1])
            {
                count++;
            }

            //same row
            if (!((col - 1) < 0) && grid[row, col - 1])
            {
                count++;
            }
            if (!((col + 1) == grid.GetLength(1)) && grid[row, col + 1])
            {
                count++;
            }

            //row below
            if (!((row + 1) == grid.GetLength(0)) && !((col - 1) < 0) && grid[row + 1, col - 1])
            {
                count++;
            }
            if (!((row + 1) == grid.GetLength(0)) && grid[row + 1, col])
            {
                count++;
            }
            if (!((row + 1) == grid.GetLength(0)) && !((col + 1) == grid.GetLength(1)) && grid[row + 1, col + 1])
            {
                count++;
            }

            return count;
        }
    }
}
