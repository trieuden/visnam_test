using be.DTOs;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IInvoiceDetailService
    {
        Task<List<InvoiceDetail>?> GetInvoiceDetailByInvoiceIdAsync(string invoiceId);
    }
}