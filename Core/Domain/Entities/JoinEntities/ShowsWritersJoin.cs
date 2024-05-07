using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities.JoinEntities;

public class ShowsWritersJoin
{
    [ForeignKey("Writer")]
    public Guid WriterID { get; set; }
    
    [ForeignKey("Show")]
    public Guid ShowID { get; set; }

    public Show Show { get; set; } = null!;
    public Person Writer { get; set; } = null!;
}