using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Services
{
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

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int row = (x + i + this.Rows) % this.Rows;
                    int col = (y + j + this.Cols) % this.Cols;

                    sum += grid[row, col];
                }
            }
            sum -= grid[x, y];

            return sum;
        }
    }
}
