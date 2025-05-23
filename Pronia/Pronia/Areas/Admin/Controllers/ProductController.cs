using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Interfaces;
using Pronia.Database.ViewModels;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string FOLDER_NAME = "Upload/Product";

        public ProductController(
            IWebHostEnvironment environment,
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _webHostEnvironment = environment;
        }


        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            var result = View(products);
            return result;
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductViewModel product)
        {
            if (!ModelState.IsValid) return View(product);
            await _productRepository.Insert(product);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var product = await _productRepository.GetById(id);
            var model = new UpdateProductViewModel()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int? id, UpdateProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model is null && id is null) return RedirectToAction(nameof(NotFound));

            await _productRepository.Update(id, model);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        { 
            _productRepository.RemoveById(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region NotFound
        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }
        #endregion
    }
}
