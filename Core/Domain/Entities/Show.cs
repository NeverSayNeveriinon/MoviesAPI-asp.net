using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities.JoinEntities;
using Core.Enums;
// using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace Core.Domain.Entities;

public abstract class Show
{
    [Key]
    public Guid ID { get; set; }
    
    [StringLength(50, ErrorMessage = "The 'Series/MovieDTO Name' Can't Be More Than 50 Characters")]
    public string Name { get; set; }
    
    public int PublishYear { get; set; }
    
    [StringLength(40, ErrorMessage = "The 'Country Name' Can't Be More Than 40 Characters")]
    public string? CountryName { get; set; }
    
    [StringLength(800, ErrorMessage = "The 'Summary' Can't Be More Than 800 Characters")]
    public string? Summary { get; set; }
    
    public LanguageOptions? Languages { get; set; }
    
    [StringLength(100, ErrorMessage = "The 'IMDB Page' URL Has Too Much Characters")]
    public string? IMDBPage { get; set; }
    
    public double? IMDBRating { get; set; }

    [StringLength(100, ErrorMessage = "The 'Image' Path Has Too Much Characters")]
    public string? ImagePath { get; set; }
    
    
    
    // Relations //
    #region Relations
    
    
    // With "Person(person as Writer)" ---> Show 'N'====......----'N' Writer(person)
    [JsonIgnore]
    public ICollection<Person> Writers { get; } = new List<Person>(); // Navigation to 'Person' entity
    public ICollection<ShowsWritersJoin> ShowsWritersJoin { get; } = new List<ShowsWritersJoin>(); // Navigation to 'ShowsWritersJoin' entity    
    
    
    // With "Person(person as Artist)" ---> Show 'N'----......----'N' Artist(person)
    [JsonIgnore]
    public ICollection<Person>? Artists { get; } = new List<Person>(); // Navigation to 'Person' entity
    public ICollection<ShowsArtistsJoin>? ShowsArtistsJoin { get; } = new List<ShowsArtistsJoin>(); // Navigation to 'ShowsArtistsJoin' entity  
    
    
    // With "Genre" ---> Show 'N'====......----'N' Genre
    [JsonIgnore]
    public ICollection<Genre> Genres { get; } = new List<Genre>(); // Navigation to 'Genre' entity
    public ICollection<ShowsGenresJoin> ShowsGenresJoin { get; } = new List<ShowsGenresJoin>(); // Navigation to 'ShowsGenresJoin' entity
    
    
    #endregion

}
