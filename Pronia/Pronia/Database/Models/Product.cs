using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Database.Models
{
    public class Product : BaseModel
    {
        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
