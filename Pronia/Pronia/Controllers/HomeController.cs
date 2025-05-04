using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;

        public HomeController()
        {
            _productRepository = new ProductRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.GetAll().OrderBy(p => p.Id).ToList();
            var result = View(products);
            return result;
        }
    }
}
