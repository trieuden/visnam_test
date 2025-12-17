using be.Data;
using be.DTOs;
using be.Models;
using be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Services.Implements
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly AppDbContext _context;

        public InvoiceDetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDetail>?> GetInvoiceDetailByInvoiceIdAsync(string invoiceId)
        {
            return await _context.InvoiceDetails.Where(i => i.invoiceId == invoiceId).ToListAsync();
        }

    }
}