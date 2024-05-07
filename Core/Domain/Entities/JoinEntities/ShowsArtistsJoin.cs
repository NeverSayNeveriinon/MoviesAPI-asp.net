using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities.JoinEntities;

public class ShowsArtistsJoin
{
    [ForeignKey("Artist")]
    public Guid ArtistID { get; set; }
    
    [ForeignKey("Show")]
    public Guid ShowID { get; set; }

    public Show Show { get; set; } = null!;
    public Person Artist { get; set; } = null!;
}