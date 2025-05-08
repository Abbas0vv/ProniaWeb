using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;
using Pronia.ViewModels.Home;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly SlideBannerRepository _slideBannerRepository;

        public HomeController()
        {
            _productRepository = new ProductRepository();
            _slideBannerRepository = new SlideBannerRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Products = _productRepository.GetAll().OrderBy(p => p.Id).ToList(),
                SlideBanners = _slideBannerRepository.GetAll().OrderBy(b => b.Id).ToList()
            };

            var result = View(model);
            return result;
        }
    }
}
