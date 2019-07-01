using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Services
{
    interface IGridService
    {
        int CountNeighborCells(int[,] grid, int x, int y);
    }
}
