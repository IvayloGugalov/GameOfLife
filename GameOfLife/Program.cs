using GameOfLife.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        private static int[,] _grid;
        private static int _cols;
        private static int _rows;

        private static GridService _gridService;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"Enter the number of rows you want:");
                var input = int.TryParse(Console.ReadLine(), out _rows);
                
                Console.WriteLine("Enter the number of columns you want:");
                var secondInput = int.TryParse(Console.ReadLine(), out _cols);

                if (IsInputValid(_rows, _cols))
                {
                    break;
                }
            }

            CreateGrid(_rows,_cols);
        }


        private static bool IsInputValid(int x, int y)
        {
            if (x > 0 && y > 0) return true;

            Console.WriteLine("Invalid input!");
            return false;

        }

        static void CreateGrid(int rows, int cols)
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
            int[,] updatedGrid = new int[_rows, _cols];


            //Array.Copy(grid, updatedGrid, grid.GetLength(0)*grid.GetLength(1));

            while (true)
            {
                for (int i = 0; i < updatedGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < updatedGrid.GetLength(1); j++)
                    {
                        //Alive or Dead
                        int cellState = grid[i, j];
                        
                        var count = new GridService(_rows, _cols);
                        int neighborCell = count.CountNeighborCells(grid, i, j);

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
                Thread.Sleep(700);
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


    }
}
