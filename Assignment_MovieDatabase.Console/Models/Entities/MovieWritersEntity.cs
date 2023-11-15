using System.ComponentModel.DataAnnotations;

namespace Assignment_MovieDatabase.Console.Models.Entities;

public class MovieWritersEntity
{
    [Required] public int MovieId { get; set; }
    [Required] public MovieEntity Movie { get; set; } = null!;


    [Required] public int WriterId { get; set; }
    [Required] public WriterEntity Writer { get; set; } = null!;
}
