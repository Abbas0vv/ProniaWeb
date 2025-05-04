namespace Pronia.Database.Models
{
    public class Product : BaseModel
    {
        public Product() { }

        public Product(string name, string description, decimal price, string productImage)
        {
            Name = name;
            Description = description;
            Price = price;
            ProductImage = productImage;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
    }
}
