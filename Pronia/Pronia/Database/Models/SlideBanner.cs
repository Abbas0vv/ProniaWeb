using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Database.Models
{
    public class SlideBanner : BaseEntity
    {
        [MinLength(3)]
        public string Title { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        public string? Offer { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
