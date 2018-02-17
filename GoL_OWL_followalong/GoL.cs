using System;
using System.Text;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes

namespace GoL_Tutorial
{
    public class GoL
    {
        private const int gridSize = 15;
        private bool[,] grid = new bool[gridSize, gridSize];
        private bool[,] tempGrid = new bool[gridSize, gridSize];

        public GoL()
        {
            /*
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = false;
                    tempGrid[i, j] = false;
                }
            }
             * */
            Clear();    // DRY

        }

        public void SetCellState(int row, int column)
        {
            grid[row, column] = !grid[row, column];
        }

        public void Clear()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = false;
                    tempGrid[i, j] = false;
                }
            }
        }

        public int GetCellState(int row, int column)
        {
            return grid[row, column] ? 1 : 0;
        }

        public void NextGeneration()
        {
            int numLiveNeighbors, inbr, jnbr;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    numLiveNeighbors = 0;
                    for (int iloc = -1; iloc <= 1; iloc++)
                    {
                        for (int jloc = -1; jloc <= 1; jloc++)
                        {
                            if (!(iloc == 0 && jloc == 0))
                            {
                                inbr = (iloc + i + gridSize) % gridSize;
                                jnbr = (jloc + j + gridSize) % gridSize;
                                if (grid[inbr, jnbr] == true)
                                    numLiveNeighbors++;
                            }
                        }
                    }
                    if (grid[i, j])
                    {
                        if (numLiveNeighbors == 2 || numLiveNeighbors == 3)
                            tempGrid[i, j] = true;
                        else
                            tempGrid[i, j] = false;
                    }
                    if (!grid[i, j])
                    {
                        if (numLiveNeighbors == 3)
                            tempGrid[i, j] = true;
                    }
                }
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = tempGrid[i, j];
                    tempGrid[i, j] = false;
                }
            }
        }
    }
}
