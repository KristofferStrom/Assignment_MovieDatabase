using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Repositories;

public class ActorRepository : Repo<ActorEntity>
{
    private readonly DataContext _context;
    public ActorRepository(DataContext context) : base(context)
    {
        _context = context;
    }


}
