using System.ComponentModel.DataAnnotations;

using Core.Domain.Entities;
using Core.Domain.Entities.JoinEntities;
using Core.Enums;


namespace Core.DTO.MovieDTO;

public class MovieRequest
{
    [Required]
    public Guid ID { get; set; }
    
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
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person.ID' as Director

    public List<Guid> WritersID { get; set; } = new List<Guid>(); // Foreign Keys to 'Person.ID' as Writers
    public List<Guid>? ArtistsID { get; set;} = new List<Guid>(); // Foreign Keys to 'Person.ID' as Artists
    public List<Guid> GenresID { get; set;} = new List<Guid>(); // Foreign Keys to 'Genre.ID'
    
    
    public Movie ToMovie()
    {
        Movie movie = new Movie()
        {
            ID = ID,
            Name = Name,
            PublishYear = PublishYear,
            CountryName = CountryName,
            Summary = Summary,
            Languages = Languages,
            IMDBPage = IMDBPage,
            IMDBRating = IMDBRating,
            ImagePath = ImagePath,
            Time = Time,
            
            DirectorID = DirectorID,
            ShowsWritersJoin = WritersID.Select(writerID => new ShowsWritersJoin(){WriterID = writerID,ShowID = ID}).ToList(),
            ShowsArtistsJoin = ArtistsID?.Select(artistID => new ShowsArtistsJoin(){ArtistID = artistID, ShowID = ID}).ToList(),
            ShowsGenresJoin = GenresID.Select(genreID => new ShowsGenresJoin(){GenreID = genreID, ShowID = ID}).ToList(),
        };
        
        return movie;
    }
}