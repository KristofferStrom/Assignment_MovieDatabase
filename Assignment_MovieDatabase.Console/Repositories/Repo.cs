using Assignment_MovieDatabase.Console.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assignment_MovieDatabase.Console.Repositories
{
    public abstract class Repo<TEntity> where TEntity : class
    {

        private readonly DataContext _context;

        protected Repo(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

                return entity ?? null!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;
        }
    }
}
