﻿using System;
using System.Windows.Shapes;

namespace GameOfLifeApplication.ServiceImplementation
{
    interface IShapesImplementation
    {
        void CreateTwoSquares(int rows, int cols, Rectangle[,] currentCell);
        void CreateTwoBigRectangles(int rows, int cols, Rectangle[,] currentCell);
        void CreateRandomizedGrid(int rows, int cols, Random random, Rectangle[,] currentCell);
    }
}
