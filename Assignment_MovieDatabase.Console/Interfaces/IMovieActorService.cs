using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Interfaces
{
    public interface IMovieActorService
    {
        Task<bool> AddMovieActorAsync(int actorId, int movieId);
        Task<bool> AddRangeAsync(List<MovieActorsEntity> movieActors);
    }
}
