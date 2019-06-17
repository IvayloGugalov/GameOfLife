using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        private static int[,] _grid;
        private static int _rows = 10;
        private static int _cols = 10;

        static void Main(string[] args)
        {
            CreateGrid(_rows,_cols);
        }

        static void CreateGrid(int rows, int cols)
        {
            Random random = new Random();

            int[,] grid = new int[rows, cols];

            string command = string.Empty;

            
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
            int[,] updatedGrid = new int[_rows, _cols];
            //Array.Copy(grid, updatedGrid, grid.GetLength(0)*grid.GetLength(1));
            while (command == "")
            {
                for (int i = 0; i < updatedGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < updatedGrid.GetLength(1); j++)
                    {
                        //Alive or Dead
                        int cellState = grid[i, j];
                        int neighborCell = CountNeighborCells(grid, i, j);

                        if (cellState == 0 && neighborCell == 3)
                        {
                            updatedGrid[i, j] = 1;

                            PrintCell(updatedGrid, i, j);
                        }

                        else if (cellState == 1 && (neighborCell < 2 || neighborCell > 3))
                        {
                            updatedGrid[i, j] = 0;

                            PrintCell(updatedGrid, i, j);
                        }

                        else
                        {
                            updatedGrid[i, j] = cellState;

                            PrintCell(updatedGrid, i, j);
                        }

                    }

                    Console.WriteLine();
                }

                grid = updatedGrid;


                command = Console.ReadLine();
                Console.Clear();
            }

        }


        static void PrintCell(int[,] grid, int x, int y)
        {

            if (grid[x, y] == 1)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(grid[x, y] + " ");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write(grid[x, y] + " ");
                Console.ResetColor();
            }
        }

        static int CountNeighborCells(int[,] grid, int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int row = (x + i + _rows) % _rows;
                    int col = (y + j + _cols) % _cols;

                    sum += grid[row, col];
                }
            }
            sum -= grid[x, y];

            return sum;
        }

    }
}
