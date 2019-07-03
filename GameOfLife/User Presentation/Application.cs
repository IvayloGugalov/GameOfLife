using GameOfLife.Services;

namespace GameOfLife.User_Presentation
{
    /// <summary>
    /// Create the Initialization of the program
    /// Providing the necessary classes and methods
    /// </summary>
    class Application
    {
        private UserInput _userInput;
        private  IGridService _gridService;

        public void Initialize()
        {
            _userInput = new UserInput();
            _userInput.ApplicationUserInput();

            _gridService = new GridService(_userInput.Rows, _userInput.Cols);
            _gridService.CreateGrid(_userInput.Rows, _userInput.Cols);
        }
    }
}
