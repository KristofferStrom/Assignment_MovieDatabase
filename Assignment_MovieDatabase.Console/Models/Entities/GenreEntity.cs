using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Assignment_MovieDatabase.Console.Models.Entities;

[Index(nameof(Genre), IsUnique = true)]
public class GenreEntity
{
    [Key]
    public int Id { get; set; }

    [Required, Column(TypeName = "nvarchar(50)")]
    public string Genre { get; set; } = null!;
}
