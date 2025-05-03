using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
