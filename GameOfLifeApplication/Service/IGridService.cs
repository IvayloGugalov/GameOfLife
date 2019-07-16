using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameOfLifeApplication.Service
{
    interface IGridService
    {
        int Rows { get; set; }
        int Cols { get; set; }

        int CountNeighborCells(int x, int y, int rows, int cols, Rectangle[,] currentCell);

        bool AreAliveCellsMoreThanTheDead(int allCells, int countAliveCells, int countTries);

        bool DidThePlayerWin(int countAlive, int countTries);
    }
}
