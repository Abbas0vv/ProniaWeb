using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Database.ViewModels;

public class UpdateSlideBannerViewModel
{
    [MinLength(3)]
    public string Title { get; set; }

    [MinLength(5)]
    public string Description { get; set; }
    public string? Offer { get; set; }
    public IFormFile? File { get; set; }
}
