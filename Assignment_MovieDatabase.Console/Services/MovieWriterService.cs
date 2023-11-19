using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;

namespace Assignment_MovieDatabase.Console.Services;

internal class MovieWriterService : IMovieWriterService
{
    private readonly MovieWriterRepository _movieWriterRepository;

    public MovieWriterService(MovieWriterRepository movieWriterRepository)
    {
        _movieWriterRepository = movieWriterRepository;
    }

    public async Task<bool> AddRangeAsync(List<MovieWritersEntity> movieWriters)
    {
        try
        {
            var isCreated = await _movieWriterRepository.AddRange(movieWriters);
            if (isCreated)
                return true;
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
