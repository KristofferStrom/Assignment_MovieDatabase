using Assignment_MovieDatabase.Console.ExtensionsMethods;
using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;
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
        private readonly IActorService _actorService;
        private readonly IMovieService _movieService;

        public ActorMenuService(UserInterfaceService userInterfaceService, IActorService actorService, IMovieService movieService)
        {
            _userInterfaceService = userInterfaceService;
            _actorService = actorService;
            _movieService = movieService;
        }

        public void AddMenu()
        {
            _userInterfaceService.AddHeader("Lägg till skådespelare");
            var firstName = _userInterfaceService.GetFieldInput("Förnamn");
            var lastName = _userInterfaceService.GetFieldInput("Efternamn");
            
            var actor = Task.Run(() => _actorService.CreateAsync(new ActorRegistration { FirstName = firstName, LastName = lastName })).Result;

            if(actor != null)
                _userInterfaceService.AddHeader($"{actor.FirstName} {actor.LastName} har lagts till");
            else
                _userInterfaceService.AddHeader($"Kunde inte lägga till skådespelare");

            _userInterfaceService.ReadKey();

        }

        public void ListAllMenu()
        {
           

            _userInterfaceService.AddHeader("Alla skådespelare");
            _userInterfaceService.AddTableHeader(20, "Id", "Namn", "Medverkar i");
            var allActors = Task.Run(_actorService.GetAllAsync).Result;

            foreach (var actor in allActors)
            {
                _userInterfaceService.AddTableRow($"{actor.Id}", $"{actor.Name}", $"{actor.MovieTitles.FirstOrDefault()}");

                if (actor.MovieTitles.Count > 1)
                {
                    for (int i = 1; i < actor.MovieTitles.Count; i++)
                        _userInterfaceService.AddTableRow("", "", $"{actor.MovieTitles.ElementAt(i)}");
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
                _userInterfaceService.AddHeader("Skådespelare");
                var selection = _userInterfaceService.GetSelectedOption("Sök", "Lista alla", "Lägg till ny Skådespelare","Ta bort en skådespelare", "Uppdatera en skådespelare", "Huvudmeny");

                switch (selection)
                {
                    case "1":
                        SearchMenu();
                        break;
                    case "2":
                        ListAllMenu();
                        break;
                    case "3":
                        AddMenu();
                        break;
                    case "4":
                        RemoveMenu();
                        break;
                    case "5":
                        UpdateMenu();
                        break;
                    case "6":
                        breakLoop = true;
                        break;

                }
                
            } while (breakLoop == false);
            
        }

        private void UpdateMenu()
        {
            _userInterfaceService.AddHeader("Uppdatera en skådespelare");
            var actors = Task.Run(() => _actorService.GetAllAsync()).Result;
            if(actors != null)
            {
                ListAll(actors);
                var selectedId = _userInterfaceService.GetFieldInput("Ange Id på den du vill uppdatera").GetInt("Ange Id").GetRequiredNumbers("Ange Id", actors.Select(a=> a.Id).ToList());

                var actorEntity = Task.Run(() => _actorService.GetActorAsync(selectedId)).Result;

                if(actorEntity != null)
                {
                    UpdateForm(actorEntity);
                }
            }
                
        }

        private void UpdateForm(ActorEntity actorEntity)
        {
            
            bool breakLoop = false;
            do
            {
                _userInterfaceService.AddHeader($"Uppdatera {actorEntity.FirstName} {actorEntity.LastName}");
                var selection = _userInterfaceService.GetSelectedOption("Förnamn", "Efternamn", "Lägg till filmen", "Ta bort filmer", "Tillbaka");

                switch (selection)
                {
                    case "1":
                        UpdateFirstNameForm(actorEntity);
                        break;
                    case "2":
                        UpdateLastNameForm(actorEntity);
                        break;
                    case "3":
                        AddMovieToActorForm(actorEntity);
                        break;
                    case "4":
                        RemoveMovieFromActorForm(actorEntity);
                        break;
                    case "5":
                        breakLoop = true;
                        break;
                }
            } while (breakLoop == false);
            
        }

        private void RemoveMovieFromActorForm(ActorEntity actorEntity)
        {
            throw new NotImplementedException();
        }

        private void AddMovieToActorForm(ActorEntity actorEntity)
        {
            
            var moviesFromActor = actorEntity.MovieActors.Select(ma => ma.Movie).ToList();
            var movies = Task.Run(() => _movieService.GetAllAsync()).Result.ToList();


            if(movies != null)
            {
                UpdatedMovieList(movies, moviesFromActor);
                
                while (true)
                {
                    _userInterfaceService.AddHeader("Lägg till filmer");
                    _userInterfaceService.AddTableHeader(20, "Id", "Titel");

                    foreach (var movie in movies)
                    {
                        _userInterfaceService.AddTableRow($"{movie.Id}", $"{movie.Title}");
                    }

                    var selectedId = _userInterfaceService.GetFieldInput("Ange Id").GetInt("Ange Id").GetRequiredNumbers("Ange Id (ange 0 för att avsluta)", movies.Select(m => m.Id).ToList());

                    if (selectedId == 0)
                        break;

                    actorEntity.MovieActors.Add(new MovieActorsEntity { ActorId = actorEntity.Id, MovieId = selectedId });
                    bool isUpdated = Task.Run(() => _actorService.UpdateAsync(actorEntity)).Result;

                    if (isUpdated)
                        _userInterfaceService.Print("Film tillagd"); 
                    else
                        _userInterfaceService.Print("Film kunde inte läggas till");

                    var movieToRemoveFromSelectionList = movies.FirstOrDefault(m => m.Id == selectedId);
                    if (movieToRemoveFromSelectionList != null)
                        movies.Remove(movieToRemoveFromSelectionList);
                }
            }
            
            
        }

        private void UpdatedMovieList(List<Movie> movies, List<MovieEntity> moviesFromActor)
        {
            foreach (var movie in moviesFromActor)
            {
                var m = movies.FirstOrDefault(m => m.Id == movie.Id);
                if (m != null)
                    movies.Remove(m);
            }
        }

        private void UpdateLastNameForm(ActorEntity actorEntity)
        {
            _userInterfaceService.AddHeader("Uppdatera Efternamn");
            actorEntity.LastName = _userInterfaceService.GetFieldInput("Efternamn");

            var isUpdated = Task.Run(() => _actorService.UpdateAsync(actorEntity)).Result;

            if (isUpdated)
                _userInterfaceService.Print("Efternamn uppdaterat!");
            else
                _userInterfaceService.Print("Efternamn kunde inte uppdateras");
        }

        private void UpdateFirstNameForm(ActorEntity actorEntity)
        {
            _userInterfaceService.AddHeader("Uppdatera Förnamn");
            actorEntity.FirstName = _userInterfaceService.GetFieldInput("Förnamn");

            var isUpdated = Task.Run(() => _actorService.UpdateAsync(actorEntity)).Result;

            if (isUpdated)
                _userInterfaceService.Print("Förnamn uppdaterat!");
            else
                _userInterfaceService.Print("Förnamn kunde inte uppdateras");
        }

        private void RemoveMenu()
        {
            var actors = Task.Run(() => _actorService.GetAllAsync()).Result;
            if (actors != null)
            {
                while (true)
                {
                    

                    _userInterfaceService.AddHeader("Ta bort skådespelare");

                    _userInterfaceService.AddTableHeader(20, "Id", "Namn");

                    foreach (var actor in actors)
                    {
                        _userInterfaceService.AddTableRow($"{actor.Id}", $"{actor.Name}");
                    }

                    var selectedIdToRemove = _userInterfaceService.GetFieldInput("Välj Id").GetInt("Välj Id").GetRequiredNumbers("Välj Id", actors.Select(a => a.Id).ToList());

                    if (Task.Run(() => _actorService.RemoveAsync(selectedIdToRemove)).Result)
                    {
                        _userInterfaceService.Print("Skådespelaren borttagen");
                        break;
                    }
                       

                    else
                        _userInterfaceService.Print("Skådespelaren kunde inte tas bort");
                }
            }
            else
                _userInterfaceService.Print("Finns inga skådespelare att ta bort");
        }

        public void SearchMenu()
        {
            _userInterfaceService.AddHeader("Sökning baserad på:");
            var selection = _userInterfaceService.GetSelectedOption("Förnamn", "Efternamn");

            switch(selection)
            {
                case "1":
                    SearchByFirstNameMenu();
                    break;
                case "2":
                    SearchByLastNameMenu();
                    break;
            }

        }

        private void SearchByLastNameMenu()
        {
            _userInterfaceService.AddHeader("Sökning baserad på Efternamn:");
            var searchInput = _userInterfaceService.GetFieldInput("Sök");

            var actors = Task.Run(() => _actorService.SearchByLastNameAsync(searchInput)).Result;

            ListAll(actors);
            _userInterfaceService.ReadKey();
        }

        private void SearchByFirstNameMenu()
        {
            _userInterfaceService.AddHeader("Sökning baserad på Förnamn:");
            var searchInput = _userInterfaceService.GetFieldInput("Sök");

            var actors = Task.Run( () => _actorService.SearchByFirstNameAsync(searchInput)).Result;

            ListAll(actors);
            _userInterfaceService.ReadKey();
        }

        private void ListAll(IEnumerable<Actor> actors)
        {
            _userInterfaceService.AddHeader("Skådespelare");
            _userInterfaceService.AddTableHeader(20, "Id", "Namn");

            foreach (var actor in actors)
            {
                _userInterfaceService.AddTableRow($"{actor.Id}", $"{actor.Name}" );
            }
        }
    }
}
