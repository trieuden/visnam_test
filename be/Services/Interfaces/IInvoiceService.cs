using be.DTOs;
using be.Models;

namespace be.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<Invoice>?> GetInvoicesAsync();
        Task<List<Invoice>?> GetInvoicesByUserIdAsync(string userId);
        Task<Invoice?> CreateInvoiceAsync(InvoiceCreateDto request);
    }
}