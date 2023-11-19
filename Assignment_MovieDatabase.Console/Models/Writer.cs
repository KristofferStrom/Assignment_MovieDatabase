using Assignment_MovieDatabase.Console.Models.Entities;
using Azure.Core;

namespace Assignment_MovieDatabase.Console.Models;

public class Writer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public static implicit operator Writer(WriterEntity entity)
    {
        return new Writer
        {
            Id = entity.Id,
            Name = entity.FirstName + " " + entity.LastName
        };
    }
}
