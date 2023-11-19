using Assignment_MovieDatabase.Console.Interfaces;
using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;
using Assignment_MovieDatabase.Console.Repositories;
using System.Diagnostics;

namespace Assignment_MovieDatabase.Console.Services;

public class ActorService : IActorService
{
    private readonly ActorRepository _actorRepository;

    public ActorService(ActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<ActorEntity> CreateAsync(ActorRegistration actorReg)
    {
        try
        {
            var actorEntity = await _actorRepository.CreateAsync(new ActorEntity { FirstName = actorReg.FirstName, LastName = actorReg.LastName });
            return actorEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<ActorEntity> GetActorAsync(int actorId)
    {
        try
        {
            var actorEntity = await _actorRepository.GetAsync(a => a.Id == actorId);

            return actorEntity ?? null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        try
        {
            var actorEntities = await _actorRepository.GetAllAsync();
            var actorList = new List<Actor>();

            foreach (var actorEntity in actorEntities)
            {
  
                actorList.Add(new Actor
                {
                    Id = actorEntity.Id,
                    Name = actorEntity.FirstName + " " + actorEntity.LastName,
                    MovieTitles = actorEntity.MovieActors.Select(m => m.Movie.Title).ToList(),
                });
            }
            return actorList;

      
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> RemoveAsync(int actorId)
    {
        try
        {
            if (!await _actorRepository.Exists(a => a.Id == actorId))
                return false;

            var actorEntity = await _actorRepository.GetAsync(a => a.Id == actorId);

            if(actorEntity != null)
            {
                await _actorRepository.RemoveAsync(actorEntity);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<Actor>> SearchByFirstNameAsync(string firstName)
    {
        try
        {
            var actorList = new List<Actor>();
            var actorEntities = await _actorRepository.SearchAsync(a => a.FirstName.StartsWith(firstName));
            if(actorEntities != null)
            {
                foreach (var actorEntity in actorEntities)
                {
                    actorList.Add(new Actor
                    {
                        Id = actorEntity.Id,
                        Name = actorEntity.FirstName + " " + actorEntity.LastName,
                        MovieTitles = actorEntity.MovieActors.Select(ma => ma.Movie.Title).ToList()
                    });
                }
                return actorList;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<Actor>> SearchByLastNameAsync(string lastName)
    {
        try
        {
            var actorList = new List<Actor>();
            var actorEntities = await _actorRepository.SearchAsync(a => a.LastName.StartsWith(lastName));
            if (actorEntities != null)
            {
                foreach (var actorEntity in actorEntities)
                {
                    actorList.Add(new Actor
                    {
                        Id = actorEntity.Id,
                        Name = actorEntity.FirstName + " " + actorEntity.LastName,
                        MovieTitles = actorEntity.MovieActors.Select(ma => ma.Movie.Title).ToList()
                    });
                }
                return actorList;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(ActorEntity actorEntity)
    {

        try
        {
            if (actorEntity != null)
            {
                if (!await _actorRepository.Exists(a => a.Id == actorEntity.Id))
                    return false;

                actorEntity = await _actorRepository.UpdateAsync(actorEntity);
                if (actorEntity != null)
                    return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
        
    }
}