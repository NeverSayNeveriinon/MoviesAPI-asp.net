using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Movie : Show
{
    public TimeOnly Time { get; set; }
    
    
    
    // Relations //
    #region Relations
    
    
    //                                      (Dependent)                (Principal)
    // With "Person(person as Director)" ---> Movie 'N'====......----'1' Director(person)
    [ForeignKey("Director")]
    public Guid DirectorID { get; set; }  // Foreign Key to 'Person(Director).ID'
    public Person Director { get; set; } = null!;  // Navigation to 'Person' entity
    
    
    #endregion
}