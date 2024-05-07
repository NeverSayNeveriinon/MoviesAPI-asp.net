using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities.JoinEntities;

public class ShowsGenresJoin
{
    [ForeignKey("Genre")]
    public Guid GenreID { get; set; }
    
    [ForeignKey("Show")]
    public Guid ShowID { get; set; }

    public Show Show { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}