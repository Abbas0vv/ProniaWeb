using Microsoft.EntityFrameworkCore;
using Pronia.Database.Interfaces;
using Pronia.Database.Models;
using Pronia.Database.ViewModels;
using Pronia.Helpers.Extentions;

namespace Pronia.Database.Repository
{
    public class SlideBannerRepository : ISlideBannerRepository
    {
        private ProniaDbContext _dbContext;
        private IWebHostEnvironment _environment;
        private const string FOLDER_NAME = "Upload/SlideBanner";

        public SlideBannerRepository(ProniaDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public List<SlideBanner> GetAll()
        {
            return _dbContext.SlideBanners.ToList();
        }

        public async Task Insert(CreateSlideBannerViewModel model)
        {
            var banner = new SlideBanner()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.File.CreateFile(_environment.WebRootPath, FOLDER_NAME),
                Offer = model.Offer
            };

            await _dbContext.SlideBanners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SlideBanner> GetById(int? id)
        {
            return await _dbContext.SlideBanners.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task RemoveById(int? id)
        {
            var slideBanner = await GetById(id);
            FileExtention.RemoveFile(Path.Combine(_environment.WebRootPath, FOLDER_NAME, slideBanner.ImageUrl));
            _dbContext.SlideBanners.Remove(slideBanner);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Update(int? id, UpdateSlideBannerViewModel model)
        {
            var slideBanner = await GetById(id);
            slideBanner.Title = model.Title;
            slideBanner.Description = model.Description;
            slideBanner.Offer = model.Offer;

            if (model.File is not null)
                slideBanner.ImageUrl = model.File.UpdateFile(_environment.WebRootPath, FOLDER_NAME, slideBanner.ImageUrl);


            _dbContext.SlideBanners.Update(slideBanner);
            await _dbContext.SaveChangesAsync();
        }
    }
}
