using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfLifeApplication.ViewModels;

namespace GameOfLifeApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int rows = 4;
        private const int cols = 4;
        private readonly Rectangle[,] _currentCell = new Rectangle[rows, cols];


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }

        private void Start_Game(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Width = GameCanvas.ActualWidth / rows - 1.0,
                        Height = GameCanvas.ActualHeight / cols - 1.0,
                        Fill = Brushes.ForestGreen,
                    };
                    GameCanvas.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, j * GameCanvas.ActualWidth / rows);
                    Canvas.SetTop(rectangle, i * GameCanvas.ActualHeight / cols);

                    rectangle.MouseDown += WhenMouseIsClicked;

                    _currentCell[i, j] = rectangle;
                }
            }
        }

        private void WhenMouseIsClicked(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle) sender).Fill = (((Rectangle) sender).Fill == Brushes.ForestGreen)
                ? Brushes.Red
                : Brushes.ForestGreen;
        }

        private void New_Button(object sender, RoutedEventArgs e)
        {
            int[,] grid = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int neighborCell = CountNeighborCells(i, j);

                    if (_currentCell[i,j].Fill == Brushes.Red
                        && neighborCell == 3)
                    {
                        _currentCell[i, j].Fill = Brushes.ForestGreen;
                    }

                    else if(_currentCell[i,j].Fill == Brushes.ForestGreen
                            && (neighborCell < 2 || neighborCell > 3))
                    {
                        _currentCell[i, j].Fill = Brushes.Red;
                    }

                }
            }

        }


        private int CountNeighborCells(int x, int y)
        {
            int sum = 0;

            //Cycle all the cells around the current
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int row = (x + i + rows) % rows;
                    int col = (y + j + cols) % cols;

                    //Check if the surrounding cells are Red
                    if (_currentCell[row, col].Fill == Brushes.ForestGreen)
                    {
                        sum++;
                    }
                }
            }

            //Subtract the current cell state if it is Red
            if (_currentCell[x, y].Fill == Brushes.Red)
            {
                sum--;
            }

            return sum;
        }
    }
}
