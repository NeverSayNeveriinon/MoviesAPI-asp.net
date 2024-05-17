using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
// using Newtonsoft.Json;

using Core.Domain.Entities.JoinEntities;
using Core.Enums;
using Core.Helpers;


namespace Core.Domain.Entities;

public class Person
{
    [Key]
    public Guid ID { get; set; }
    
    [StringLength(30, ErrorMessage = "The 'Person First Name' Can't Be More Than 30 Characters")]
    public string FirstName { get; set; }
    
    [StringLength(30, ErrorMessage = "The 'Person Last Name' Can't Be More Than 30 Characters")]
    public string LastName { get; set; }
    
    public GenderOptions GenderName { get; set; }
    
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? DateOfBirth { get; set; }
    
    public JobOptions JobName { get; set; }
    
    [StringLength(800, ErrorMessage = "The 'Summary' Can't Be More Than 800 Characters")]
    public string? Summary { get; set; }
    
    
    
    // Relations //
    #region Relations

    
    //                                      (Dependent)                (Principal)
    // With "MovieDTO(person as Director)" ---> MovieDTO 'N'====......----'1' Director(person)
    public ICollection<Movie>? MoviesDirected { get; } = new List<Movie>();

    
    // With "Show(person as Writer)" ---> Show 'N'====......----'N' Writer(person)
    public ICollection<Show>? ShowsWritten { get; } = new List<Show>(); // Navigation to 'Show' entity
    public ICollection<ShowsWritersJoin>? ShowsWritersJoin { get; } = new List<ShowsWritersJoin>(); // Navigation to 'ShowsWritersJoin' entity
    
    // With "Show(person as Artist)" ---> Show 'N'----......----'N' Artist(person)
    public ICollection<Show>? ShowsPlayed { get; } = new List<Show>(); // Navigation to 'Show' entity
    public ICollection<ShowsArtistsJoin>? ShowsArtistsJoin { get; } = new List<ShowsArtistsJoin>(); // Navigation to 'ShowsArtistsJoin' entity
    
    
    // With "Serial(person as Director)" ---> Serial 'N'====......----'N' Director(person)
    public ICollection<Serial>? SeriesDirected { get; } = new List<Serial>(); // Navigation to 'Serial' entity
    public ICollection<SeriesDirectorsJoin>? SeriesDirectorsJoin { get; } = new List<SeriesDirectorsJoin>(); // Navigation to 'SeriesDirectorsJoin' entity
    

    #endregion
}