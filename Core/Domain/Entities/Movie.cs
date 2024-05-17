using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
// using Newtonsoft.Json;

using Core.Helpers;


namespace Core.Domain.Entities;

public class Movie : Show
{
    [JsonConverter(typeof(TimeOnlyJsonConverter))]
    public TimeOnly Time { get; set; }
    
    
    
    // Relations //
    #region Relations
    
    
    //                                      (Dependent)                (Principal)
    // With "Person(person as Director)" ---> MovieDTO 'N'====......----'1' Director(person)
    [ForeignKey("Director")]
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person(Director).ID'
    // [JsonIgnore]
    public Person Director { get; set; } = null!;  // Navigation to 'Person' entity
    
    
    #endregion
}