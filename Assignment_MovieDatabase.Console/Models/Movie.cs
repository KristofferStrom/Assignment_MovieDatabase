using Assignment_MovieDatabase.Console.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Assignment_MovieDatabase.Console.Models;

namespace Assignment_MovieDatabase.Console;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Language { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string Director { get; set; } = null!;
    public int AgeLimit { get; set; }
    public ICollection<string> Actors { get; set; } = new List<string>();
    public ICollection<string> Writers { get; set; } = new List<string>();
}
