using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia.Database;
using Pronia.Database.Models.Account;
using Pronia.ViewModels.Account;
namespace Pronia.Controllers;
public class AccountController : Controller
{

    private readonly UserManager<ProniaUser> _userManager;
    private readonly SignInManager<ProniaUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ProniaUser> userManager,
        SignInManager<ProniaUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View();

        var user = new ProniaUser()
        {
            Name = registerViewModel.Name,
            Surname = registerViewModel.Surname,
            Email = registerViewModel.Email,
            UserName = registerViewModel.Username
        };

        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
                ModelState.AddModelError("", item.Description);

            return View();
        }

        await _signInManager.SignInAsync(user, true);
        return RedirectToAction("Index", "Home");
    }
}
