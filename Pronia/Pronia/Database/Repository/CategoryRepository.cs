using Pronia.Database.Models;

namespace Pronia.Database.Repository;

public class CategoryRepository
{
    private ProniaDbContext _dbContext;

    public CategoryRepository(ProniaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Category> GetAll()
    {
        return _dbContext.Categories.OrderBy(c => c.Id).ToList();
    }

}
