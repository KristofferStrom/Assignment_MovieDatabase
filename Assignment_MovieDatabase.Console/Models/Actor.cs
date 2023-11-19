using Assignment_MovieDatabase.Console.Models.Entities;
using System.Text;

namespace Assignment_MovieDatabase.Console.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<string> MovieTitles { get; set; } = new List<string>();


    //public static implicit operator Actor(ActorEntity entity)
    //{
    //    return new Actor
    //    {
    //        Id = entity.Id,
    //        Name = entity.FirstName + " " + entity.LastName,
    //        Movies = entity.MovieActors.
    //    }
    //}

    public string GetFormattedMovieTitles()
    {
        var sb = new StringBuilder();

        foreach (var movie in MovieTitles)
        {
            sb.AppendLine(movie);
        }

        return sb.ToString();

    }
}
