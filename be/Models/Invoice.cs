namespace be.Models
{
    public class Invoice
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string userId { get; set; } = string.Empty;
        public DateTime dateCreated { get; set; } = DateTime.UtcNow;
        public decimal totalAmount { get; set; }
    }

}