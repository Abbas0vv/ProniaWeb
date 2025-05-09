using Pronia.Database.Models;

namespace Pronia.Database.Repository;

public class AppUserReposityory
{
    private ProniaDbContext _dbContext;

    public AppUserReposityory(ProniaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public List<AppUser> GetAll()
    //{
    //    return _dbContext.AppUsers.OrderBy(c => c.Id).ToList();
    //}
}
