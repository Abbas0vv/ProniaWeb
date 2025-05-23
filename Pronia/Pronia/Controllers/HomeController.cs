using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Interfaces;
using Pronia.ViewModels.Home;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISlideBannerRepository _slideBannerRepository;
        public HomeController(
            IProductRepository productRepository,
            ISlideBannerRepository slideBannerRepository)
        {
            _productRepository = productRepository;
            _slideBannerRepository = slideBannerRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Products = _productRepository.GetAll().OrderBy(p => p.Id).ToList(),
                SlideBanners = _slideBannerRepository.GetAll().OrderBy(b => b.Id).ToList(),
            };

            var result = View(model);
            return result;
        }
    }
}
