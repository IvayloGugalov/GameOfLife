using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLifeApplication.Service
{
    class CreateShapes: ICreateShapes
    {
        public void CreateTwoSquares(int rows, int cols, Rectangle[,] currentCell)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    for (int q = -1; q < 5; q++)
                    {
                        for (int p = -1; p < 5; p++)
                        {
                            currentCell[rows + q - 4, cols + p - 4].Fill = Brushes.ForestGreen;
                        }
                    }

                    for (int q = -1; q < 5; q++)
                    {
                        for (int p = -1; p < 5; p++)
                        {
                            currentCell[rows + q + 1, cols + p + 1].Fill = Brushes.ForestGreen;
                        }
                    }
                }
            }
        }

        public void CreateTwoBigRectangles(int rows, int cols, Rectangle[,] currentCell)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i < rows && j < cols)
                    {
                        currentCell[i, j].Fill = Brushes.ForestGreen;
                        currentCell[rows + i, cols + j].Fill = Brushes.ForestGreen;
                    }
                }
            }
        }

        public void CreateRandomizedGrid(int rows, int cols, Random random, Rectangle[,] currentCell)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var randomChoice = random.Next(0, 2);
                    if (randomChoice == 1)
                    {
                        currentCell[i, j].Fill = Brushes.ForestGreen;
                    }
                }
            }
        }
    }
}
