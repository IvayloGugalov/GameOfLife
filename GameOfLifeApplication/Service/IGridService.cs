using System.Windows.Shapes;

namespace GameOfLifeApplication.Service
{
    interface IGridService
    {
        int Rows { get; set; }
        int Cols { get; set; }

        int CountNeighborCells(int x, int y, int rows, int cols, Rectangle[,] currentCell);

        bool DidThePlayerWin(int countAlive, int countTries);
    }
}
