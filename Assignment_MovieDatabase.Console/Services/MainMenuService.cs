using System;
using Assignment_MovieDatabase.Console.Interfaces;

namespace Assignment_MovieDatabase.Console.Services
{
    internal class MainMenuService : IMainMenuService
    {
        private readonly UserInterfaceService _userInterface;
        private readonly IActorMenuService _actorMenuService;
        private readonly IMovieMenuService _movieMenuService;



        public MainMenuService(UserInterfaceService userInterface, IActorMenuService actorMenuService, IMovieMenuService movieMenuService)
        {
            _userInterface = userInterface;
            _actorMenuService = actorMenuService;
            _movieMenuService = movieMenuService;
        }

        public void MainMenu()
        {
            do
            {
                _userInterface.AddHeader("Meny");
                var selection = _userInterface.GetSelectedOption("Hantera Filmer", "Hantera Skådespelare");

                switch (selection)
                {
                    case "1":
                        _movieMenuService.Menu();   
                        break;
                    case "2":
                        _actorMenuService.Menu();
                        break;
                    
                }
            }
            while (true);
        }
    }
}
