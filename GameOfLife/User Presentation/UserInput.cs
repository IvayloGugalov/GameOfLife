using System;

namespace GameOfLife.User_Presentation
{
    /// <summary>
    /// Start up screen for the user
    /// Initialization of the row and column components
    /// </summary>
    class UserInput
    {
        public int Rows { get; set; }
        public int Cols { get; set; }

        public (int, int) ApplicationUserInput()
        {
            int rows = 0;
            int cols = 0;

            //A while loop that breaks when the user has inputted correct data
            while (true)
            {
                Console.WriteLine($"Enter the number of rows you want:");
                var input = Int32.TryParse(Console.ReadLine(), out rows);

                Console.WriteLine("Enter the number of columns you want:");
                var secondInput = Int32.TryParse(Console.ReadLine(), out cols);

                if (IsInputValid(rows, cols))
                {
                    break;
                }
            }

            this.Rows = rows;
            this.Cols = cols;

            return (this.Rows, this.Cols);
        }

        private static bool IsInputValid(int x, int y)
        {
            //Validate if the user input is a positive number
            if (x > 0 && y > 0) return true;

            Console.WriteLine("Invalid input!");
            return false;
        }
    }
}
