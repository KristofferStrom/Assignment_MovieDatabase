using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class MovieWriterRepository : Repo<MovieWritersEntity>
    {
        private readonly DataContext _context;
        public MovieWriterRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieWritersEntity>> GetWhereAsync(Expression<Func<MovieWritersEntity, bool>> predicate)
        {
            try
            {
                return await _context.MovieWriters.Include(ma => ma.Movie).Include(ma => ma.Writer).Where(predicate).ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<bool> AddRange(List<MovieWritersEntity> movieWriters)
        {
            try
            {
                if (movieWriters != null)
                {
                    _context.MovieWriters.AddRange(movieWriters);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false!;
        }


    }
}
