using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;

namespace Assignment_MovieDatabase.Console.Services
{
    public class MovieActorService : IMovieActorService
    {
        private readonly MovieActorRepository _repository;

        public MovieActorService(MovieActorRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddMovieActorAsync(int actorId, int movieId)
        {
            try
            {
               var movieActorEntity = await _repository.CreateAsync(new MovieActorsEntity { ActorId = actorId, MovieId = movieId});
                if (movieActorEntity != null)
                    return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }

        public async Task<bool> AddRangeAsync(List<MovieActorsEntity> movieActors)
        {
            try
            {
                var isCreated = await _repository.AddRange(movieActors);
                
                return isCreated;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }
    }
}
