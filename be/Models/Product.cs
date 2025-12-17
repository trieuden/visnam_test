namespace be.Models
{
    public class Product
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; } = string.Empty;
        public decimal price { get; set; }
        public string imageUrl { get; set; } = string.Empty;
    }
}