namespace Pronia.Database.Models
{
    public class SlideBanner : BaseModel
    {
        public SlideBanner(string title, string description, string offer)
        {
            Title = title;
            Description = description;
            Offer = offer;
        }
        public SlideBanner(string title, string description)
        {
            Title = title;
            Description = description;
        }
        public SlideBanner() { }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Offer { get; set; }
    }
}
