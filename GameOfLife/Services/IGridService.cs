namespace GameOfLife.Services
{
    interface IGridService
    {
        int CountNeighborCells(int[,] grid, int x, int y);

        void CreateGrid(int rows, int cols);

        void PrintCell(int[,] grid, int x, int y);
    }
}
