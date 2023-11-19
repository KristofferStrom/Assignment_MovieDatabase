using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public class MovieActorRepository : Repo<MovieActorsEntity>
    {
        private readonly DataContext _dataContext;
        public MovieActorRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public override async Task<IEnumerable<MovieActorsEntity>> GetAllAsync()
        {
            try
            {
                return await _dataContext.MovieActors.Include(ma => ma.Movie).Include(ma=> ma.Actor).ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public async Task<IEnumerable<MovieActorsEntity>> GetWhereAsync(Expression<Func<MovieActorsEntity, bool>> predicate)
        {
            try
            {
                return await _dataContext.MovieActors.Include(ma => ma.Movie).Include(ma => ma.Actor).Where(predicate).ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<bool> AddRange(List<MovieActorsEntity> movieActors)
        {
            try
            {
                if(movieActors != null)
                {
                    _dataContext.MovieActors.AddRange(movieActors);
                    await _dataContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false!;
        }

       
    }
}
