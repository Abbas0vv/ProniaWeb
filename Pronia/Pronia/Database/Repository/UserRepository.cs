
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.Database.Interfaces;
using Pronia.Database.Models.Account;
using Pronia.Helpers.Enums;
using Pronia.ViewModels.Account;

namespace Pronia.Database.Repository;

public class UserRepository : IUserRepository
{

    private readonly UserManager<ProniaUser> _userManager;
    private readonly SignInManager<ProniaUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<ProniaUser> userManager,
        SignInManager<ProniaUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task Register(RegisterViewModel model)
    {
        var count = await _userManager.Users.CountAsync();
        var user = new ProniaUser()
        {
            Name = model.Name,
            Surname = model.Surname,
            UserName = model.Username,
            Email = model.Email
        };


        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            if (count == 0)
                await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
            else
                await _userManager.AddToRoleAsync(user, Role.User.ToString());

            await _signInManager.SignInAsync(user, true);
        }
    }

    public async Task Login(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is not null)
        {
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
                await _signInManager.SignInAsync(user, true);
        }
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task CreateRole()
    {
        foreach (var item in Enum.GetValues(typeof(Role)))
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = item.ToString()
            });
        }
    }
}
