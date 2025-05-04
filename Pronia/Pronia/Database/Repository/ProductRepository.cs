using Pronia.Database.Models;

namespace Pronia.Database.Repository
{
    public class ProductRepository
    {
        private ProniaDbContext _dbContext;

        public ProductRepository()
        {
            _dbContext = new ProniaDbContext();
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public void Insert(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveById(int id)
        {
            try
            {
                var product = GetById(id);
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
            catch (NullReferenceException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
