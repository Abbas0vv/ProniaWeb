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
            var products = _productRepository.GetAll();
            var result = View(products);
            return result;
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, decimal price, string description)
        {
            Product product = new Product(name, description, price);
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
        public IActionResult Update(int id, string name, decimal price, string description)
        {
            var product = _productRepository.GetById(id);

            if (product is null) return RedirectToAction(nameof(NotFound));

            product.Name = name;
            product.Price = price;
            product.Description = description;

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
