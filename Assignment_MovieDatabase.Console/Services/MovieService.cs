using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;

namespace Assignment_MovieDatabase.Console.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieEntity> AddMovieAsync(MovieRegistration movie)
        {
            try
            {
                var movieEntity = new MovieEntity
                {
                     Title = movie.Title,
                     Description = movie.Description,
                     DirectorId = movie.DirectorId,
                     GenreId = movie.GenreId,
                     AgeLimitId = movie.AgeLimitId,
                     LanguageId = movie.LanguageId,
                };
                movieEntity = await _movieRepository.CreateAsync(movieEntity);

                if(movieEntity != null)
                    return movieEntity;
                
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
			try
			{
                var movieList = new List<Movie>();
                var moviesEntities = await _movieRepository.GetAllAsync();
                foreach (var movie in moviesEntities)
                {
                    movieList.Add(new Movie
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Language = movie.Language.Language,
                        Description = movie.Description,
                        AgeLimit = movie.AgeLimit.AgeLimit,
                        Genre = movie.Genre.Genre,
                        Director = movie.Director.FirstName + " " + movie.Director.LastName,
                        Actors = movie.MovieActors.Select(ma=> ma.Actor.FirstName + " " + ma.Actor.LastName).ToList(),
                        Writers = movie.MovieWriters.Select(mw => mw.Writer.FirstName + " " + mw.Writer.LastName).ToList()

                    }) ;
                }
                return movieList;
			}
			catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public async Task<bool> RemoveAsync(int movieId)
        {
            try
            {
                if (!await _movieRepository.Exists(m => m.Id == movieId))
                    return false;

                var movieEntity = await _movieRepository.GetAsync(a => a.Id == movieId);

                if (movieEntity != null)
                {
                    await _movieRepository.RemoveAsync(movieEntity);
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }
    }
}
