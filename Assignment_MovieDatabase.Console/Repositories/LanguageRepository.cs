using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Repositories;

public class LanguageRepository : Repo<LanguageEntity>
{
    private readonly DataContext _context;
    public LanguageRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
