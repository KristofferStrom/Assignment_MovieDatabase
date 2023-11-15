using System.ComponentModel.DataAnnotations;

namespace Assignment_MovieDatabase.Console.Models.Entities;

public class MovieActorsEntity
{
    [Required] public int MovieId { get; set; }
    [Required] public MovieEntity Movie { get; set; } = null!;


    [Required] public int ActorId { get; set; }
    [Required] public ActorEntity Actor { get; set; } = null!;
}
