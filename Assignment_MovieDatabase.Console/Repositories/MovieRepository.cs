using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class MovieRepository : Repo<MovieEntity>
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MovieEntity>> GetAllAsync()
        {
            try
            {
                var movieEntities = await _context.Movies.Include(m => m.AgeLimit).Include(m=> m.Director).Include(m=>m.Genre).Include(m=>m.Language).Include(m => m.MovieActors).ThenInclude(ma => ma.Actor).Include(m=> m.MovieWriters).ThenInclude(mw=> mw.Writer).ToListAsync();

                return movieEntities ?? null!;
              
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }
    }
}
