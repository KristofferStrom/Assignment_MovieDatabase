using Assignment_MovieDatabase.Console.Models;
using Assignment_MovieDatabase.Console.Models.Entities;
using Assignment_MovieDatabase.Console.Models.Registrations;
using System.Linq.Expressions;

namespace Assignment_MovieDatabase.Console.Interfaces
{
    public interface IActorService
    {
        Task<ActorEntity> CreateAsync(ActorRegistration actorReg);
        Task<ActorEntity> GetActorAsync(int actorId);
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<bool> RemoveAsync(int actorId);
        Task<IEnumerable<Actor>> SearchByFirstNameAsync(string firstName);
        Task<IEnumerable<Actor>> SearchByLastNameAsync(string lastName);
        Task<bool> UpdateAsync(ActorEntity actorEntity);
    }
}
