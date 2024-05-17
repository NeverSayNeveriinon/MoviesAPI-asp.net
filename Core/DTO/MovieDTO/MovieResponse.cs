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
    
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person.ID' as Director
    public string? DirectorName { get; set; } = null;  
    
    
    public List<Guid> WritersID { get; set; } = new List<Guid>(); // Foreign Keys to 'Person.ID' as Writers
    public List<Guid>? ArtistsID { get; set; } = new List<Guid>(); // Foreign Keys to 'Person.ID' as Artists
    public List<Guid> GenresID { get; set; } = new List<Guid>(); // Foreign Keys to 'Genre.ID'
    
    
}

public static class MovieExtensions
{
    public static MovieResponse ToMovieResponse (this Movie movie)
    {
        MovieResponse response = new MovieResponse()
        {
            ID = movie.ID,
            Name = movie.Name,
            PublishYear = movie.PublishYear,
            CountryName = movie.CountryName,
            Summary = movie.Summary,
            Languages = movie.Languages,
            IMDBPage = movie.IMDBPage,
            IMDBRating = movie.IMDBRating,
            ImagePath = movie.ImagePath,
            Time = movie.Time,
            
            DirectorName = movie.Director?.FirstName + " " + movie.Director?.LastName,
            
            DirectorID = movie.DirectorID,
            WritersID = movie.Writers.Select(writerItem => writerItem.ID).ToList(),
            ArtistsID = movie.Artists?.Select(artistItem => artistItem.ID).ToList(),
            GenresID = movie.Genres.Select(genreItem => genreItem.ID).ToList(),
        };

        return response;
    }
}
