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
        private const int rows = 40;
        private const int cols = 40;
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
                        Fill = Brushes.MediumAquamarine,
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
            ((Rectangle) sender).Fill = (((Rectangle) sender).Fill ==
                                         Brushes.MediumAquamarine)
                ? Brushes.Red
                : Brushes.MediumAquamarine;
        }

        private void New_Button(object sender, RoutedEventArgs e)
        {
            int[,] grid = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int sum = 0;

                    int neighborCell = CountNeighborCells(grid, i, j);

                    if (_currentCell[i,j].Fill == Brushes.Red
                        && neighborCell == 3)
                    {
                        _currentCell[i, j].Fill = Brushes.MediumAquamarine;
                    }

                    else if(_currentCell[i,j].Fill == Brushes.MediumAquamarine
                            && (neighborCell < 2 || neighborCell > 3))
                    {
                        _currentCell[i, j].Fill = Brushes.Red;
                    }

                }
            }

        }


        private int CountNeighborCells(int[,] grid, int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (_currentCell[i, j].Fill == Brushes.Red)
                    {
                        sum++;
                    }
                    else
                    {
                        sum--;
                    }

                    int row = (x + i + rows) % rows;
                    int col = (y + j + cols) % cols;

                    sum += grid[row, col];
                }
            }
            sum -= grid[x, y];

            return sum;
        }
    }
}
