using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Interfaces;

public interface IMovieWriterService
{
    Task<bool> AddRangeAsync(List<MovieWritersEntity> movieWriters);
}
