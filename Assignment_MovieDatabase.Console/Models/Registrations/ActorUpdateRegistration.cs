using Assignment_MovieDatabase.Console.Models.Entities;

namespace Assignment_MovieDatabase.Console.Models.Registrations;

public class ActorUpdateRegistration
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<MovieActorsEntity> MovieActors { get; set; } = new List<MovieActorsEntity>();
}
