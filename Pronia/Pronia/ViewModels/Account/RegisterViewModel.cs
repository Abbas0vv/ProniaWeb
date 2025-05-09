using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels.Account;

public class RegisterViewModel
{
    [MinLength(3)]
    public string Name { get; set; }
    [MinLength(3)]
    public string Surname { get; set; }

    [MinLength(3)]
    public string Username { get; set; }

    [DataType(DataType.EmailAddress), RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public string Email { get; set; }
    [MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare(nameof(Password)), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
