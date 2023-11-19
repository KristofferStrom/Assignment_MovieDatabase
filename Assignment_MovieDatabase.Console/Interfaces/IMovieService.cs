using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;

namespace Assignment_MovieDatabase.Console.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<MovieEntity> AddMovieAsync(MovieRegistration movie);
        Task<bool> RemoveAsync(int movieId);
    }
}
