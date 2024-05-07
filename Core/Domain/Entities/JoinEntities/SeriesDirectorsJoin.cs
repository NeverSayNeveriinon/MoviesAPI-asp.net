using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities.JoinEntities;

public class SeriesDirectorsJoin
{
    [ForeignKey("Director")]
    public Guid DirectorID { get; set; }
    
    [ForeignKey("Serial")]
    public Guid SerialID { get; set; }

    public Serial Serial { get; set; } = null!;
    public Person Director { get; set; } = null!;
}