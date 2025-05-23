
using Pronia.ViewModels.Account;

namespace Pronia.Database.Interfaces;

public interface IUserRepository
{
    Task Register(RegisterViewModel model);
    Task Login(LoginViewModel model);
    Task LogOut();
    Task CreateRole();
}
