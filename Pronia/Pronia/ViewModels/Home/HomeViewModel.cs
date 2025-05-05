using Pronia.Database.Models;

namespace Pronia.ViewModels.Home;

public class HomeViewModel
{
    public List<Product> Products { get; set; }
    public List<SlideBanner> SlideBanners { get; set; }  
}
