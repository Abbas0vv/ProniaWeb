using Microsoft.AspNetCore.Mvc;
using Pronia.Database.Models;
using Pronia.Database.Repository;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController()
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

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, decimal price, string description, string productImage)
        {
            Product product = new Product(name, description, price, productImage);
            _productRepository.Insert(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));
     
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(int id, string name, decimal price, string description, string productImage)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));

            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.ProductImage = productImage;

            _productRepository.Update(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));

            _productRepository.RemoveById(id);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
