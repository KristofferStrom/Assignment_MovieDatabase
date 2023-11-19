using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class GenreRepository : Repo<GenreEntity>
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
