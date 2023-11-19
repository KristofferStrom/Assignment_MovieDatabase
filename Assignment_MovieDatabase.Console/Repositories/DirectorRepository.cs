using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Repositories;

public class DirectorRepository : Repo<DirectorEntity>
{
    private readonly DataContext _context;
    public DirectorRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
