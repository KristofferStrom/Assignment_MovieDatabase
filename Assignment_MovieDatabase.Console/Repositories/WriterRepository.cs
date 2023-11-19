using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class WriterRepository : Repo<WriterEntity>
    {
        private readonly DataContext _context;
        public WriterRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
