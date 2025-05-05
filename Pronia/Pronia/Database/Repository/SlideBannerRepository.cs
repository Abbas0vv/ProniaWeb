using Pronia.Database.Models;

namespace Pronia.Database.Repository
{
    public class SlideBannerRepository
    {
        private ProniaDbContext _dbContext;

        public SlideBannerRepository()
        {
            _dbContext = new ProniaDbContext();
        }
        public List<SlideBanner> GetAll()
        {
            return _dbContext.SlideBanners.ToList();
        }

        public async Task Insert(SlideBanner slideBanner)
        {
            await _dbContext.SlideBanners.AddAsync(slideBanner);
            await _dbContext.SaveChangesAsync();
        }

        public SlideBanner GetById(int id)
        {
            return _dbContext.SlideBanners.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveById(int id)
        {
            try
            {
                var slideBanner = GetById(id);
                _dbContext.SlideBanners.Remove(slideBanner);
                _dbContext.SaveChanges();
            }
            catch (NullReferenceException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(SlideBanner slideBanner)
        {
            _dbContext.SlideBanners.Update(slideBanner);
            _dbContext.SaveChanges();
        }
    }
}
