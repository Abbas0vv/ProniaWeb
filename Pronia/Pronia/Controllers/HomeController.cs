using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Repository;
using Pronia.ViewModels.Home;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly SlideBannerRepository _slideBannerRepository;
        private readonly CategoryRepository _categoryRepository;
        public HomeController(
            ProductRepository productRepository,
            SlideBannerRepository slideBannerRepository,
            CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _slideBannerRepository = slideBannerRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Products = _productRepository.GetAll().OrderBy(p => p.Id).ToList(),
                SlideBanners = _slideBannerRepository.GetAll().OrderBy(b => b.Id).ToList(),
                Categories = _categoryRepository.GetAll().OrderBy(c => c.Id).ToList()
            };

            var result = View(model);
            return result;
        }
    }
}
