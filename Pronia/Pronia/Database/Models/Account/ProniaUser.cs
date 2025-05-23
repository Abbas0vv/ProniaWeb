using Microsoft.AspNetCore.Identity;

namespace Pronia.Database.Models.Account;

public class ProniaUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
