using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;

namespace Assignment_MovieDatabase.Console.Services;

public class WriterService : IWriterService
{
    private readonly WriterRepository _writerRepository;

    public WriterService(WriterRepository writerRepository)
    {
        _writerRepository = writerRepository;
    }

    public async Task<IEnumerable<Writer>> GetAllAsync()
    {
        try
        {
            var writerEntities = await _writerRepository.GetAllAsync();
            if(writerEntities != null)
                return writerEntities.Select(w => new Writer { Id = w.Id, Name = w.FirstName + " " + w.LastName});
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
   
        return null!;
    }
}
