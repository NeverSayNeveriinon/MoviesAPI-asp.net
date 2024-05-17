using System.ComponentModel.DataAnnotations;

using Core.Domain.Entities.JoinEntities;
using Core.Enums;


namespace Core.Domain.Entities;

public class Genre
{
    [Key]
    public Guid ID { get; set; }
    
    public GenreOptions Name { get; set; }
    
    [StringLength(800, ErrorMessage = "The 'Summary' Can't Be More Than 800 Characters")]
    public string? Summary { get; set; }
    

    
    // Relations //
    #region Relations
    

    // With "Show" ---> Show 'N'====......----'N' Genre
    public ICollection<Show>? Shows { get; } = new List<Show>(); // Navigation to 'Show' entity
    public ICollection<ShowsGenresJoin>? ShowsGenresJoin { get; } = new List<ShowsGenresJoin>(); // Navigation to 'ShowsGenresJoin' entity

    
    #endregion
}