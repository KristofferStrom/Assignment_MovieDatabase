using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Assignment_MovieDatabase.Console.Models.Entities;

[Index(nameof(AgeLimit), IsUnique = true)]
public class AgeLimitEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AgeLimit { get; set; }
}
