using System.ComponentModel.DataAnnotations;
using Core.Enums;
using Core.Domain.Entities;

namespace Core.DTO.MovieDTO;

public class MovieRequest
{
    [Required(ErrorMessage = "The 'MovieDTO Name' Can't Be Blank!!!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The 'Publish Year' Can't Be Blank!!!")]
    public int PublishYear { get; set; }
    
    public string? CountryName { get; set; }
    
    public string? Summary { get; set; }
    
    public LanguageOptions? Languages { get; set; }
    
    public string? IMDBPage { get; set; }
    
    public double? IMDBRating { get; set; }

    public string? ImagePath { get; set; }
    
    [Required(ErrorMessage = "The 'Time' Can't Be Blank!!!")]
    public TimeOnly Time { get; set; }
    
    [Required(ErrorMessage = "The 'Director' Can't Be not Selected!!!")]
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person(Director).ID'

    // public ICollection<Person> Writers { get; } = new List<Person>(); // Navigation to 'Person' entity
    // public ICollection<Person>? Artists { get; } = new List<Person>(); // Navigation to 'Person' entity
    // public ICollection<Genre> Genres { get; } = new List<Genre>(); // Navigation to 'Genre' entity
    
    
    public Movie ToMovie()
    {
        Movie movie = new Movie()
        {
            ID = Guid.NewGuid(),
            PublishYear = PublishYear,
            CountryName = CountryName,
            Summary = Summary,
            Languages = Languages,
            IMDBPage = IMDBPage,
            IMDBRating = IMDBRating,
            ImagePath = ImagePath,
            Time = Time,
            DirectorID = DirectorID
        };
        return movie;
    }
}