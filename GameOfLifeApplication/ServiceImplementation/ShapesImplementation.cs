using System;
using System.Windows.Shapes;
using GameOfLifeApplication.Service;

namespace GameOfLifeApplication.ServiceImplementation
{
    class ShapesImplementation: IShapesImplementation
    {
        private readonly ICreateShapes _createShapes;

        public ShapesImplementation()
        {
            _createShapes = new CreateShapes();
        }

        public void CreateTwoSquares(int rows, int cols, Rectangle[,] currentCell)
        {
            _createShapes.CreateTwoSquares(rows, cols, currentCell);
        }

        public void CreateTwoBigRectangles(int rows, int cols, Rectangle[,] currentCell)
        {
            _createShapes.CreateTwoBigRectangles(rows, cols, currentCell);
        }

        public void CreateRandomizedGrid(int rows, int cols, Random random, Rectangle[,] currentCell)
        {
            _createShapes.CreateRandomizedGrid(rows, cols, random, currentCell);
        }
    }
}
