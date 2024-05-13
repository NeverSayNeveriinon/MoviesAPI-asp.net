using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Core.Enums;

namespace Core.DTO.MovieDTO;

public class MovieResponse
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public int PublishYear { get; set; }
    public string? CountryName { get; set; }
    public string? Summary { get; set; }
    public LanguageOptions? Languages { get; set; }
    public string? IMDBPage { get; set; }
    public double? IMDBRating { get; set; }
    public string? ImagePath { get; set; }
    public TimeOnly Time { get; set; }
    
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person(Director).ID'
    
    // Additional
    public string DirectorName { get; set; }  

    // public ICollection<Person> Writers { get; } = new List<Person>(); // Navigation to 'Person' entity
    // public ICollection<Person>? Artists { get; } = new List<Person>(); // Navigation to 'Person' entity
    // public ICollection<Genre> Genres { get; } = new List<Genre>(); // Navigation to 'Genre' entity
    
    
}

public static class MovieExtensions
{
    public static MovieResponse ToMovieResponse (this Movie movie)
    {
        MovieResponse response = new MovieResponse()
        {
            ID = movie.ID,
            PublishYear = movie.PublishYear,
            CountryName = movie.CountryName,
            Summary = movie.Summary,
            Languages = movie.Languages,
            IMDBPage = movie.IMDBPage,
            IMDBRating = movie.IMDBRating,
            ImagePath = movie.ImagePath,
            Time = movie.Time,
            DirectorID = movie.DirectorID,
            
            DirectorName = movie.Director.FirstName + movie.Director.LastName,
        };

        return response;
    }
}
