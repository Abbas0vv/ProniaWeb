using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Database.Models;
using Pronia.Database.Repository;
using Pronia.Helpers.Extentions;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string FOLDER_NAME = "Upload/Product";

        public ProductController(
            IWebHostEnvironment environment,
            ProductRepository productRepository,
            CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            if (product.File is null)
            {
                ModelState.AddModelError("File", "Please upload an image.");
                return View(product);
            }
            if (!product.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "The uploaded file must be an image.");
                return View(product);
            }
            if (product.File.Length > 2097152)
            {
                ModelState.AddModelError("File", "The uploaded file size must not exceed 2 MB.");
                return View(product);
            }
            ViewBag.Categories = _categoryRepository.GetAll();

            product.ImageUrl = product.File.CreateFile(_webHostEnvironment.WebRootPath, FOLDER_NAME);

            await _productRepository.Insert(product);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            if (product is null) return RedirectToAction(nameof(NotFound));


            var existingProduct = _productRepository.GetById(product.Id);
            if (existingProduct == null) return RedirectToAction(nameof(NotFound));

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            if (!product.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "The uploaded file must be an image.");
                return View(product);
            }
            if (product.File.Length > 2097152)
            {
                ModelState.AddModelError("File", "The uploaded file size must not exceed 2 MB.");
                return View(product);
            }
            FileExtention.RemoveFile(_webHostEnvironment.WebRootPath, FOLDER_NAME, existingProduct.ImageUrl);

            var newImageUrl = product.File.CreateFile(_webHostEnvironment.WebRootPath, FOLDER_NAME);
            existingProduct.ImageUrl = newImageUrl;

            _productRepository.Update(existingProduct);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));

            FileExtention.RemoveFile(_webHostEnvironment.WebRootPath, FOLDER_NAME, product.ImageUrl);
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
