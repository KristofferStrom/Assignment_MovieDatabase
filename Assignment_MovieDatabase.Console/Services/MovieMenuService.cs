using Assignment_MovieDatabase.Console.ExtensionsMethods;
using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;
using System.IO;

namespace Assignment_MovieDatabase.Console.Services;

public class MovieMenuService : IMovieMenuService
{
    private readonly UserInterfaceService _userInterfaceService;
    private readonly IMovieService _movieService;
    private readonly IActorService _actorService;
    private readonly IMovieActorService _movieActorService;
    private readonly IWriterService _writerService;
    private readonly IMovieWriterService _movieWriterService;
    private readonly GenreRepository _genreRepository;
    private readonly LanguageRepository _languageRepository;
    private readonly AgeLimitRepository _ageLimitRepository;
    private readonly DirectorRepository _directorRepository;



    public MovieMenuService(UserInterfaceService userInterfaceService, IMovieService movieService, GenreRepository genreRepository, LanguageRepository languageRepository, AgeLimitRepository ageLimitRepository, DirectorRepository directorRepository, IActorService actorService, IMovieActorService movieActorService, IWriterService writerService, IMovieWriterService movieWriterService)
    {
        _userInterfaceService = userInterfaceService;
        _movieService = movieService;
        _genreRepository = genreRepository;
        _languageRepository = languageRepository;
        _ageLimitRepository = ageLimitRepository;
        _directorRepository = directorRepository;
        _actorService = actorService;
        _movieActorService = movieActorService;
        _writerService = writerService;
        _movieWriterService = movieWriterService;
    }

    public void AddMenu()
    {
        _userInterfaceService.AddHeader("Lägg till film");
        var title = _userInterfaceService.GetFieldInput("Titel");
        var description = _userInterfaceService.GetFieldInput("Filmbeskrivning");

        try
        {
            var genreId = GetSelectedGenreId();
            var languageId = GetSelectedLanguageId();
            var ageLimitId = GetSelectedAgeLimitId();
            var directorId = GetSelectedDirectorId();

            var movieEntity = Task.Run(() => _movieService.AddMovieAsync(new MovieRegistration
            {
                Title = title,
                Description = description,
                GenreId = genreId,
                LanguageId = languageId,
                AgeLimitId = ageLimitId,
                DirectorId = directorId
            })).Result;

            if(movieEntity != null )
            {
                var movieActorList = GetSelectedMovieActors(movieEntity.Id);

                if (movieActorList.Count > 0)
                {
                    var isCreated = Task.Run(() => _movieActorService.AddRangeAsync(movieActorList)).Result;
                    if (isCreated)
                    {
                        _userInterfaceService.AddHeader(movieEntity.Title);
                        _userInterfaceService.Print("Skådespelare tillagda");
                    }
                    else
                    {
                        _userInterfaceService.AddHeader(movieEntity.Title);
                        _userInterfaceService.Print("Kunde inte lägga till skådespelare");
                    }

                }

                var movieWriterList = GetSelectedMovieWriters(movieEntity.Id);

                if (movieWriterList.Count > 0)
                {
                    var isCreated = Task.Run(() => _movieWriterService.AddRangeAsync(movieWriterList)).Result;
                    if (isCreated)
                    {
                        _userInterfaceService.AddHeader(movieEntity.Title);
                        _userInterfaceService.Print("Manusförfattare tillagda");
                    }
                    else
                    {
                        _userInterfaceService.AddHeader(movieEntity.Title);
                        _userInterfaceService.Print("Kunde inte lägga till manusförfattare");
                    }

                }
            }
            else
            {
                _userInterfaceService.Print("Något gick fel. Filmen kunde inte läggas till.");
            }
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

    }

    private List<MovieWritersEntity> GetSelectedMovieWriters(int movieId)
    {
        var movieWriterList = new List<MovieWritersEntity>();

        var writers = Task.Run(() => _writerService.GetAllAsync()).Result.ToList();

        while (true)
        {
            _userInterfaceService.AddHeader("Ange Manusförfattare");
            _userInterfaceService.AddTableHeader(20, "Id", "Namn");

            if (writers != null)
            {

                foreach (var writer in writers)
                    _userInterfaceService.AddTableRow($"{writer.Id}", $"{writer.Name}");
       
                var writerId = _userInterfaceService.GetFieldInput("Välj Id (ange 0 för att hoppa över)").GetInt("Välj Id").GetRequiredNumbers("Välj Id", writers.Select(w=> w.Id).ToList());

                if (writerId == 0)
                    break;

                var writerToRemoveFromList = writers.FirstOrDefault(w => w.Id == writerId);
                if(writerToRemoveFromList != null )
                    writers.Remove(writerToRemoveFromList);

                movieWriterList.Add(new MovieWritersEntity { MovieId = movieId, WriterId = writerId });
            }
        }
        return movieWriterList;
    }

    private List<MovieActorsEntity> GetSelectedMovieActors(int movieId)
    {
        var movieActorList = new List<MovieActorsEntity>();

        var actors = Task.Run(() => _actorService.GetAllAsync()).Result.ToList();

        while (true)
        {
            _userInterfaceService.AddHeader("Ange Skådespelare");
            _userInterfaceService.AddTableHeader(20, "Id", "Namn");

            if (actors != null)
            {
                foreach (var actor in actors)
                    _userInterfaceService.AddTableRow($"{actor.Id}", $"{actor.Name}");

                var actorId = _userInterfaceService.GetFieldInput("Välj Id (ange 0 för att hoppa över)").GetInt("Välj Id").GetRequiredNumbers("Välj Id", actors.Select(a => a.Id).ToList());

                if (actorId == 0)
                    break;

                var actorToRemoveFromSelectionList = actors.FirstOrDefault(w => w.Id == actorId);
                if (actorToRemoveFromSelectionList != null)
                    actors.Remove(actorToRemoveFromSelectionList);

                movieActorList.Add(new MovieActorsEntity { MovieId = movieId, ActorId = actorId });
            }
        }
        return movieActorList;
    }

    private int GetSelectedDirectorId()
    {
        _userInterfaceService.AddHeader("Ange Regissör");
        _userInterfaceService.AddTableHeader(20, "Id", "Förnamn", "Efternamn");

        var directors = Task.Run(() => _directorRepository.GetAllAsync()).Result;

        foreach (var director in directors)
        {
            _userInterfaceService.AddTableRow($"{director.Id}", $"{director.FirstName}", $"{director.LastName}");
        }

        return _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", directors.Select(d => d.Id).ToList());
    }

    private int GetSelectedAgeLimitId()
    {
        _userInterfaceService.AddHeader("Ange Åldersgräns");
        _userInterfaceService.AddTableHeader(20, "Id", "Åldersgräns");

        var ageLimits = Task.Run(() => _ageLimitRepository.GetAllAsync()).Result;

        foreach (var ageLimit in ageLimits)
        {
            _userInterfaceService.AddTableRow($"{ageLimit.Id}", $"{ageLimit.AgeLimit}");
        }

        return _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", ageLimits.Select(a => a.Id).ToList());
    }

    private int GetSelectedLanguageId()
    {
        _userInterfaceService.AddHeader("Ange språk");
        _userInterfaceService.AddTableHeader(20, "Id", "Språk");

        var languages = Task.Run(() => _languageRepository.GetAllAsync()).Result;

        foreach (var language in languages)
        {
            _userInterfaceService.AddTableRow($"{language.Id}", $"{language.Language}");
        }

        return _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", languages.Select(l => l.Id).ToList());


    }

    private int GetSelectedGenreId()
    {
        _userInterfaceService.AddHeader("Ange Genre");
        _userInterfaceService.AddTableHeader(20, "Id", "Genre");

        var genres = Task.Run(() => _genreRepository.GetAllAsync()).Result;

        foreach (var genre in genres)
        {
            _userInterfaceService.AddTableRow($"{genre.Id}", $"{genre.Genre}");
        }

        return _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", genres.Select(g => g.Id).ToList());
    }

    public void ListAllMenu()
    {
        _userInterfaceService.AddHeader("Alla filmer");
        _userInterfaceService.AddTableHeader(20, "Id", "Titel", "Genre", "Regissör", "Åldersgräns", "Språk", "Manusförfattare", "Skådespelare");

        var movies = Task.Run(() => _movieService.GetAllAsync()).Result;

        foreach ( var movie in movies )
        {
            _userInterfaceService.AddTableRow($"{movie.Id}", $"{movie.Title}", $"{movie.Genre}", $"{movie.Director}", $"{movie.AgeLimit}", $"{movie.Language}", $"{movie.Writers.FirstOrDefault()}", $"{movie.Actors.FirstOrDefault()}");
            if (movie.Actors.Count > 1 || movie.Writers.Count > 1)
            {
                if(movie.Actors.Count > movie.Writers.Count)
                {
                    for (int i = 1; i < movie.Actors.Count; i++)
                    {
                        if (i < movie.Writers.Count)
                            _userInterfaceService.AddTableRow("", "", "", "", "", "", $"{movie.Writers.ElementAt(i)}", $"{movie.Actors.ElementAt(i)}");
                        else
                            _userInterfaceService.AddTableRow("", "", "", "", "", "", "", $"{movie.Actors.ElementAt(i)}");
                    }
                   
                        
                }
                else if(movie.Actors.Count < movie.Writers.Count)
                {
                    for (int i = 1; i < movie.Writers.Count; i++)
                    {
                        if (i < movie.Actors.Count)
                            _userInterfaceService.AddTableRow("", "", "", "", "", "", $"{movie.Writers.ElementAt(i)}", $"{movie.Actors.ElementAt(i)}");
                        else
                            _userInterfaceService.AddTableRow("", "", "", "", "", "", $"{movie.Writers.ElementAt(i)}", "");
                    }
                   
                }
                else
                {
                    for (int i = 1; i < movie.Writers.Count; i++)
                        _userInterfaceService.AddTableRow("", "", "", "", "", "", $"{movie.Writers.ElementAt(i)}", $"{movie.Actors.ElementAt(i)}");
                }
                
            }
            _userInterfaceService.AddDivider();

        }
        _userInterfaceService.ReadKey();
    }

    public void Menu()
    {
        var breakLoop = false;
        do
        {
            _userInterfaceService.AddHeader("Filmer");
            var selection = _userInterfaceService.GetSelectedOption("Lista alla", "Lägg till ny", "Ta bort", "Huvudmeny");

            switch (selection)
            {
                case "1":
                    ListAllMenu();
                    break;
                case "2":
                    AddMenu();
                    break;
                case "3":
                    RemoveMenu();
                    break;
                case "4":
                    breakLoop = true;
                    break;

            }
        } while (breakLoop == false);
    }

    private void RemoveMenu()
    {
        var movies = Task.Run(() => _movieService.GetAllAsync()).Result;
        if (movies != null)
        {
            while (true)
            {


                _userInterfaceService.AddHeader("Ta bort film");

                _userInterfaceService.AddTableHeader(20, "Id", "Titel");

                foreach (var movie in movies)
                {
                    _userInterfaceService.AddTableRow($"{movie.Id}", $"{movie.Title}");
                }

                var selectedIdToRemove = _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", movies.Select(a => a.Id).ToList());

                if (Task.Run(() => _movieService.RemoveAsync(selectedIdToRemove)).Result)
                {
                    _userInterfaceService.Print("Filmen borttagen");
                    break;
                }
                    

                else
                    _userInterfaceService.Print("Filmen kunde inte tas bort");
            }
        }
        else
            _userInterfaceService.Print("Finns ingen film att ta bort");
    }
}
