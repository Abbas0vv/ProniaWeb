using Pronia.Database.Models;
using Pronia.Database.ViewModels;

namespace Pronia.Database.Interfaces;

public interface ISlideBannerRepository
{
    List<SlideBanner> GetAll();
    Task Insert(CreateSlideBannerViewModel slideBanner);
    Task<SlideBanner> GetById(int? id);
    Task RemoveById(int? id);
    Task Update(int? id, UpdateSlideBannerViewModel slideBanner);
}
