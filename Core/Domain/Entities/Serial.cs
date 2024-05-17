using System.ComponentModel.DataAnnotations;

using Core.Domain.Entities.JoinEntities;


namespace Core.Domain.Entities;

public class Serial : Show
{
    [StringLength(30, ErrorMessage = "The 'Channel Name' Can't Be More Than 30 Characters")]
    public string? PublishChannel { get; set; }
    
    public int EpisodesCount { get; set; }
    public int SeasonsCount { get; set; }
    
    
    
    // Relations //
    #region Relations
    
    
    // With "Person(person as Director)" ---> Serial 'N'====......----'N' Director(person)
    public ICollection<Person> Directors { get; } = new List<Person>(); // Navigation to 'Person' entity
    public ICollection<SeriesDirectorsJoin> SeriesDirectorsJoin { get; } = new List<SeriesDirectorsJoin>(); // Navigation to 'SeriesDirectorsJoin' entity    
    
    
    #endregion
}