using System.ComponentModel.DataAnnotations;

using Core.Domain.Entities.JoinEntities;
using Core.Enums;


namespace Core.Domain.Entities;

public abstract class Show
{
    [Key]
    public Guid ID { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }
    
    public int PublishYear { get; set; }
    
    [StringLength(40)]
    public string? CountryName { get; set; }
    
    [StringLength(800)]
    public string? Summary { get; set; }
    
    public LanguageOptions? Languages { get; set; }
    
    [StringLength(100)]
    public string? IMDBPage { get; set; }
    
    [Range(0, 10)]
    public double? IMDBRating { get; set; }

    [StringLength(100)]
    public string? ImagePath { get; set; }
    
    
    
    // Relations //
    #region Relations
    
    
    // With "Person(person as Writer)" ---> Show 'N'====......----'N' Writer(person)
    // [JsonIgnore]
    public ICollection<Person> Writers { get; set;  } = new List<Person>(); // Navigation to 'Person' entity
    public ICollection<ShowsWritersJoin> ShowsWritersJoin { get; set; } = new List<ShowsWritersJoin>(); // Navigation to 'ShowsWritersJoin' entity    
    
    
    // With "Person(person as Artist)" ---> Show 'N'----......----'N' Artist(person)
    // [JsonIgnore]
    public ICollection<Person>? Artists { get; set;  } = new List<Person>(); // Navigation to 'Person' entity
    public ICollection<ShowsArtistsJoin>? ShowsArtistsJoin { get; set; } = new List<ShowsArtistsJoin>(); // Navigation to 'ShowsArtistsJoin' entity  
    
    
    // With "Genre" ---> Show 'N'====......----'N' Genre
    // [JsonIgnore]
    public ICollection<Genre> Genres { get; set;  } = new List<Genre>(); // Navigation to 'Genre' entity
    public ICollection<ShowsGenresJoin> ShowsGenresJoin { get; set; } = new List<ShowsGenresJoin>(); // Navigation to 'ShowsGenresJoin' entity
    
    
    #endregion

}
