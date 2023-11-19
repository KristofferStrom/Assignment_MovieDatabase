namespace Assignment_MovieDatabase.Console.Models.Registrations;

public class MovieRegistration
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int LanguageId { get; set; }
    public int GenreId { get; set; }
    public int DirectorId { get; set; }
    public int AgeLimitId { get; set; }

}
