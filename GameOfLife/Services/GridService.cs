using System;
using System.Threading;

namespace GameOfLife.Services
{
    /// <summary>
    /// Implementing the IGridService 
    /// </summary>
    public class GridService : IGridService
    {
        public int Rows { get; set; }
        public int Cols { get; set; }

        public GridService(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
        }

        public int CountNeighborCells(int[,] grid, int x, int y)
        {
            int sum = 0;

            //Cycle to check the state of the surrounding cells
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    //Make a wrap around the grid
                    int row = (x + i + this.Rows) % this.Rows;
                    int col = (y + j + this.Cols) % this.Cols;

                    sum += grid[row, col];
                }
            }
            sum -= grid[x, y];

            return sum;
        }

        public void CreateGrid(int rows, int cols)
        {
            Random random = new Random();

            int[,] grid = new int[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col] = random.Next(0, 2);
                    PrintCell(grid, row, col);
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            //Initializing the updated grid
            int[,] updatedGrid = new int[this.Rows, this.Cols];

            //A while loop to animate the grid when it changes 
            while (true)
            {
                for (int i = 0; i < updatedGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < updatedGrid.GetLength(1); j++)
                    {
                        //Check if Cell is Alive or Dead
                        int cellState = grid[i, j];

                        var count = new GridService(this.Rows, this.Cols);

                        //Get the neighbor cells state
                        int neighborCell = count.CountNeighborCells(grid, i, j);

                        switch (cellState)
                        {
                            //Change cell state from dead to alive
                            case 0 when neighborCell == 3:
                                updatedGrid[i, j] = 1;
                                PrintCell(updatedGrid, i, j);
                                break;

                            //Change cell state from alive to dead
                            case 1 when (neighborCell < 2 || neighborCell > 3):
                                updatedGrid[i, j] = 0;
                                PrintCell(updatedGrid, i, j);
                                break;

                            //The state of the cell remains the same
                            default:
                                updatedGrid[i, j] = cellState;
                                PrintCell(updatedGrid, i, j);
                                break;
                        }
                    }

                    Console.WriteLine();
                }

                grid = updatedGrid;
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        //Printing the cells: Green for alive cells and Red for the rest
        public void PrintCell(int[,] grid, int x, int y)
        {
            if (grid[x, y] == 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(grid[x, y] + " ");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(grid[x, y] + " ");
                Console.ResetColor();
            }
        }
    }
}
