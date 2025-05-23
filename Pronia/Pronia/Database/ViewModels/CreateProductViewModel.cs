using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Database.ViewModels;

public class CreateProductViewModel
{
    [MinLength(3)]
    public string Name { get; set; }

    [MinLength(5)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile File { get; set; }

}
