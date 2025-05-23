using Pronia.Database.Models;

namespace Pronia.Database.Repository;

public class AppUserRepository
{
    private ProniaDbContext _dbContext;

    public AppUserRepository(ProniaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public List<AppUser> GetAll()
    //{
    //    return _dbContext.AppUsers.OrderBy(c => c.Id).ToList();
    //}
}
