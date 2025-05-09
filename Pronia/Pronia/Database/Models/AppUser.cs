using Microsoft.AspNetCore.Identity;

namespace Pronia.Database.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
