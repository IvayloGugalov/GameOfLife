using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameOfLifeApplication.Service;

namespace GameOfLifeApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGridService _gridService;
        private readonly ICreateShapes _createShapes = new CreateShapes();

        private const int rows = 20;
        private const int cols = 20;
        private readonly Rectangle[,] _currentCell = new Rectangle[rows, cols];

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private bool _isGamePaused = false;
        public bool _isGameLoaded = false;
        private int _countAlive = 0;
        private int _countTries = 0;


        public MainWindow()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(0.2);
            _timer.Tick += Start_Game;
        }

        private void Load_Game(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //Create the Grid
                    Rectangle rectangle = new Rectangle
                    {
                        Width = GameCanvas.ActualWidth / rows - 0.5,
                        Height = GameCanvas.ActualHeight / cols - 0.5,
                        Fill = Brushes.Red,
                    };
                    GameCanvas.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, j * GameCanvas.ActualWidth / rows);
                    Canvas.SetTop(rectangle, i * GameCanvas.ActualHeight / cols);

                    //Switch the color
                    rectangle.MouseEnter += WhenMouseEntersCell;

                    _currentCell[i, j] = rectangle;
                }
            }

            _isGameLoaded = true;
        }

        /// <summary>
        /// Switch the color of a cell when the mouse enters it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void WhenMouseEntersCell(object sender, MouseEventArgs mouseEventArgs)
        {
            ((Rectangle) sender).Fill = (((Rectangle) sender).Fill == Brushes.ForestGreen)
                ? Brushes.Red
                : Brushes.ForestGreen;
        }

        private void Pause_Game(object sender, EventArgs e)
        {
            _isGamePaused = true;
        }

        private void Start_Game(object sender, EventArgs e)
        {
            //Start game
            _timer.Start();
            Functions();

            if (!_isGamePaused)
            {
                return;
            }

            _timer.Stop();

            StartGame.Content = "Resume game";

            _isGamePaused = false;

            if (_isGamePaused)
            {
                _timer.Start();
            }

        }

        private void Functions()
        {
            _gridService = new GridService
            {
                Rows = rows,
                Cols = cols
            };

            SwitchCellState();
            
            //If the player didn't win, stop the game
            if (_gridService.DidThePlayerWin(_countAlive, _countTries))
            {
                StartGame.Content = "Start game";
                LoadContent.Content = "Reload game";
                //StartGame.IsEnabled = false;

                _timer.Stop();
                _isGameLoaded = false;
                _countTries = 0;
                _countAlive = 0;
            }
            _countAlive = 0;
        }

        private void SwitchCellState()
        {
            //Go through the grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    //Check the surrounding cells
                    int neighborCell = _gridService.CountNeighborCells(i, j, rows, cols, _currentCell);

                    //Change cell state from dead to alive
                    if (_currentCell[i, j].Fill == Brushes.Red && neighborCell == 3)
                    {
                        _currentCell[i, j].Fill = Brushes.ForestGreen;
                        //_countAlive++;
                    }

                    //Change cell state from alive to dead
                    else if (_currentCell[i, j].Fill == Brushes.ForestGreen &&(neighborCell < 2 || neighborCell > 3))
                    {
                        _currentCell[i, j].Fill = Brushes.Red;
                    }

                    //Doesn't work correctly, need a second variable...
                    else
                    {
                        _currentCell[i, j].Fill = _currentCell[i, j].Fill;
                        if (_currentCell[i, j].Fill == Brushes.ForestGreen)
                        {
                            _countAlive++;
                        }
                    }
                }
            }

            _countTries++;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var choice = ComboBox.SelectedItem;
            int firstRows = rows / 2;
            int firstCols = cols / 2;
            Random random = new Random();

            if (Equals(choice, Square))
            {
                _createShapes.CreateTwoSquares(firstRows, firstCols, _currentCell);
            }

            else if(Equals(choice, BigRectangle))
            {
                _createShapes.CreateTwoBigRectangles(firstRows, firstCols, _currentCell);
            }

            else if (Equals(choice, RandomCells))
            {
                _createShapes.CreateRandomizedGrid(rows, cols, random, _currentCell);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonPanel.Visibility = Visibility.Visible;
            IntroPanel.Visibility = Visibility.Hidden;
            InformationText.Visibility = Visibility.Visible;
        }
    }
}
