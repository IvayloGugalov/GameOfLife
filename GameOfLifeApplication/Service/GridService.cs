using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLifeApplication.Service
{
    class GridService: IGridService
    {
        private readonly double _thirtyFivePercent = 0.35;
        public int Rows { get; set; }
        public int Cols { get; set; }

        public int CountNeighborCells(int x, int y, int rows, int cols, Rectangle[,] currentCell)
        {
            int sum = 0;

            //Cycle all the cells around the current
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int row = (x + i + rows) % rows;
                    int col = (y + j + cols) % cols;

                    //Check if the surrounding cells are Alive
                    if (currentCell[row, col].Fill == Brushes.ForestGreen)
                    {
                        sum++;
                    }
                }
            }

            //Subtract the current cell state if it is Alive
            if (currentCell[x, y].Fill == Brushes.ForestGreen)
            {
                sum--;
            }

            return sum;
        }

        public bool AreAliveCellsMoreThanTheDead(int allCells, int countAliveCells, int countTries)
        {
            double moreThan_thirtyFivePercentCells = allCells * _thirtyFivePercent;

            //Check if the alive cells are more than half of all cells
            if (moreThan_thirtyFivePercentCells <= countAliveCells )
            {
                MessageBox.Show($"There are 35% alive cells.\nYou won the game!!!\nYou won with {countTries} tries.");
                return true;
            }

            return false;
        }

        public bool DidThePlayerWin(int countAlive, int countTries)
        {
            if (AreAliveCellsMoreThanTheDead((this.Rows * this.Cols), countAlive, countTries))
            {
                return true;
            }

            if (countTries == 50)
            {
                MessageBox.Show("You exceeded the limit of 50 tries and didn't win.");
                return true;
            }

            return false;
        }
    }
}
