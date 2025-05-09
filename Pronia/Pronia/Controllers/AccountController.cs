using Microsoft.AspNetCore.Mvc;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.ViewModels.Account;
namespace Pronia.Controllers;
public class AccountController : Controller
{
    ProniaDbContext _dbContext;
    public AccountController(ProniaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View();

        AppUser user = new AppUser()
        {
            Name = registerViewModel.Name,
            Surname = registerViewModel.Surname,
            Email = registerViewModel.Email,
            UserName = registerViewModel.Username
        };  
        return RedirectToAction(nameof(Index),nameof(HomeController));
    }
}
