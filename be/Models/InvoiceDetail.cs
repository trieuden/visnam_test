namespace be.Models
{
    public class InvoiceDetail
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string invoiceId { get; set; } = string.Empty;
        public string productId { get; set; } = string.Empty;
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
    }
}