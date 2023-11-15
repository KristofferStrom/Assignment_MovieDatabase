using Assignment_MovieDatabase.Console.Interfaces;

namespace Assignment_MovieDatabase.Console.Services;

public class MovieMenuService : IMovieMenuService
{
    private readonly UserInterfaceService _userInterfaceService;

    public MovieMenuService(UserInterfaceService userInterfaceService)
    {
        _userInterfaceService = userInterfaceService;
    }

    public void AddMenu()
    {
        throw new NotImplementedException();
    }

    public void Menu()
    {
        _userInterfaceService.AddHeader("Filmer");
        _userInterfaceService.GetSelectedOption("Sök", "Lista alla", "Sortera");
    }
}
