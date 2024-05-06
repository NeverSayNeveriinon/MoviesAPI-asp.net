using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{
    [StringLength(30, ErrorMessage = "The 'Person Name' Can't Be More Than 30 Characters")]
    public string? PersonName { get; set; }
}