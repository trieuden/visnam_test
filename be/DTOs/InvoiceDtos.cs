namespace be.DTOs
{
    public class InvoiceDetailDtos
    {
        public string productId { get; set; } = string.Empty;
        public int quantity { get; set; }
    }
    public class InvoiceCreateDto
    {
        public string userId { get; set; } = string.Empty;
        public DateTime invoiceDate { get; set; } = DateTime.UtcNow;
        public decimal totalAmount { get; set; }

        public List<InvoiceDetailDtos> invoiceDetails { get; set; } = new List<InvoiceDetailDtos>();

    }
}