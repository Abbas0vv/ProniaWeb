using Pronia.Database.Models;

namespace Pronia.Database.Repository
{
    public class ProductRepository
    {
        private static readonly List<Product> _products = new List<Product>();
    

    public List<Product> GetAll()
        {
            return _products;
        }

        public void Insert(Product product)
        {
            _products.Add(product);
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveById(int id)
        {
            _products.RemoveAll(b => b.Id == id);
        }
    }
}
