using Assignment_MovieDatabase.Console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_MovieDatabase.Console.Services
{
    public class ActorMenuService : IActorMenuService
    {
        private readonly UserInterfaceService _userInterfaceService;

        public ActorMenuService(UserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public void AddMenu()
        {
            _userInterfaceService.AddHeader("Skådespelare");
            var firstName = _userInterfaceService.GetFieldInput("Förnamn");
            var lastName = _userInterfaceService.GetFieldInput("Efternamn");
           
        }

        public void Menu()
        {
            _userInterfaceService.AddHeader("Skådespelare");
            var breakLoop = false;
            do
            {
                var selection = _userInterfaceService.GetSelectedOption("Sök", "Lista alla", "Lägg till ny Skådespelare", "Huvudmeny");

                switch (selection)
                {
                    case "1":
                        var hej = "hej";
                        break;
                    case "2":
                        var hej2 = "hej";
                        break;
                    case "3":
                        AddMenu();
                        break;
                    case "4":
                        breakLoop = true;
                        break;
                        
                }
                
            } while (breakLoop == false);
            
        }
    }
}
