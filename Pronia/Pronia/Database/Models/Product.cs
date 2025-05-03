namespace Pronia.Database.Models
{
    public class Product : BaseModel
    {
        private static int _id = 1;
        public Product(string name, string description, decimal price)
        {
            Id = _id++;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
