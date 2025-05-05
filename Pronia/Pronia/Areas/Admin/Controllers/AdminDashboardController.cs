using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
