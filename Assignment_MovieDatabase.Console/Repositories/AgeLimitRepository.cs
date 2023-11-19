using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class AgeLimitRepository : Repo<AgeLimitEntity>
    {
        private readonly DataContext _context;
        public AgeLimitRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
