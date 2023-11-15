using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Assignment_MovieDatabase.Console.Models.Entities;

[Index(nameof(Language), IsUnique = true)]
public class LanguageEntity
{
    [Key]
    public int Id { get; set; }

    [Required, Column(TypeName = "nvarchar(50)")]
    public string Language { get; set; } = null!;
}
