using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_MovieDatabase.Console.Models.Entities;

public class DirectorEntity
{
    [Key]
    public int Id { get; set; }

    [Required, Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required, Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;
}
