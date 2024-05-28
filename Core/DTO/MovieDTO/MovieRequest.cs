using System.ComponentModel.DataAnnotations;

using Core.Domain.Entities;
using Core.Domain.Entities.JoinEntities;
using Core.Enums;
using Core.Helpers.CustomValidateAttributes;


namespace Core.DTO.MovieDTO;

public class MovieRequest
{
    [Required]
    public Guid? ID { get; set; }
    
    [Required(ErrorMessage = "The 'Movie Name' Can't Be Blank!!!")]
    [StringLength(50, ErrorMessage = "The 'Series/Movie Name' Can't Be More Than 50 Characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The 'Publish Year' Can't Be Blank!!!")]
    public int? PublishYear { get; set; }
    
    [StringLength(40, ErrorMessage = "The 'Country Name' Can't Be More Than 40 Characters")]
    public string? CountryName { get; set; }
    
    [StringLength(800, ErrorMessage = "The 'Summary' Can't Be More Than 800 Characters")]
    public string? Summary { get; set; }
    
    public LanguageOptions? Languages { get; set; }
    
    [StringLength(100, ErrorMessage = "The 'IMDB Page' URL Has Too Much Characters")]
    public string? IMDBPage { get; set; }
    
    [Range(0, 10)]
    public double? IMDBRating { get; set; }

    [StringLength(100, ErrorMessage = "The 'Image' Path Has Too Much Characters")]
    public string? ImagePath { get; set; }
    
    [Required(ErrorMessage = "The 'Time' Can't Be Blank!!!")]
    public TimeOnly? Time { get; set; }
    
    
    
    [Required(ErrorMessage = "The 'Director' Can't Be not Selected!!!")]
    public Guid? DirectorID { get; set; }  // Foreign Key to 'Person.ID' as Director
    
    [MinLengthRequired(1, ErrorMessage = "At Least One 'Writer' Has to Be Selected!!!")]
    public List<Guid?> WritersID { get; set; } // Foreign Keys to 'Person.ID' as Writers
    
    public List<Guid>? ArtistsID { get; set;} // Foreign Keys to 'Person.ID' as Artists
    
    [MinLengthRequired(1, ErrorMessage = "At Least One 'Genre' Has to Be Selected!!!")]
    public List<Guid?> GenresID { get; set;} // Foreign Keys to 'Genre.ID'
    
    
    
    public Movie ToMovie()
    {
        Movie movie = new Movie()
        {
            ID = ID.GetValueOrDefault(),
            Name = Name,
            PublishYear = PublishYear.GetValueOrDefault(),
            CountryName = CountryName,
            Summary = Summary,
            Languages = Languages,
            IMDBPage = IMDBPage,
            IMDBRating = IMDBRating,    
            ImagePath = ImagePath,
            Time = Time.GetValueOrDefault(),
            
            DirectorID = DirectorID.GetValueOrDefault(),
            ShowsWritersJoin = WritersID.Select(writerID => new ShowsWritersJoin(){WriterID = writerID.GetValueOrDefault(),ShowID = ID.GetValueOrDefault()}).ToList(),
            ShowsArtistsJoin = ArtistsID?.Select(artistID => new ShowsArtistsJoin(){ArtistID = artistID, ShowID = ID.GetValueOrDefault()}).ToList(),
            ShowsGenresJoin = GenresID.Select(genreID => new ShowsGenresJoin(){GenreID = genreID.GetValueOrDefault(), ShowID = ID.GetValueOrDefault()}).ToList(),
        };
        
        return movie;
    }
}