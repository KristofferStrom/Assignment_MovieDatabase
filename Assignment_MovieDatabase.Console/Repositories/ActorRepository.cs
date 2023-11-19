using Assignment_MovieDatabase.Console.Contexts;
using Assignment_MovieDatabase.Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assignment_MovieDatabase.Console.Repositories;

public class ActorRepository : Repo<ActorEntity>
{
    private readonly DataContext _context;
    public ActorRepository(DataContext context) : base(context)
    {
        _context = context;
    }


    public override async Task<ActorEntity> CreateAsync(ActorEntity entity)
    {
        try
        {
            var hej = _context.Actors.Add(entity);

            await _context.SaveChangesAsync();

            entity = _context.Actors.Include(a=> a.MovieActors).ThenInclude(a => a.Movie).FirstOrDefault(a => a.Id == entity.Id);

            return entity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;


    }

    public override async Task<IEnumerable<ActorEntity>> GetAllAsync()
    {
        try
        {
           return await _context.Actors.Include(a => a.MovieActors).ThenInclude(a=>a.Movie).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<ActorEntity>> SearchAsync(Expression<Func<ActorEntity, bool>> predicate)
    {
        try
        {
            return await _context.Actors.Include(a => a.MovieActors).ThenInclude(a => a.Movie).Where(predicate).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
}
