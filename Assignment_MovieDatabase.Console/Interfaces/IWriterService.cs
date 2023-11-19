using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Interfaces;

public interface IWriterService
{
    Task<IEnumerable<Writer>> GetAllAsync();
}
