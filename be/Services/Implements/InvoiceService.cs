using be.Data;
using be.DTOs;
using be.Models;
using be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace be.Services.Implements
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>?> GetInvoicesAsync()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<List<Invoice>?> GetInvoicesByUserIdAsync(string userId)
        {
            return await _context.Invoices
                .Where(i => i.userId == userId)
                .ToListAsync();
        }

        public async Task<Invoice?> CreateInvoiceAsync(InvoiceCreateDto request)
        {
            var invoice = new Invoice
            {
                id = Guid.NewGuid().ToString(),
                userId = request.userId,
                totalAmount = request.totalAmount
            };

            _context.Invoices.Add(invoice);

            foreach (var detail in request.invoiceDetails)
            {
                var product = await _context.Products.FindAsync(detail.productId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.productId} not found.");
                }

                var invoiceDetail = new InvoiceDetail
                {
                    id = Guid.NewGuid().ToString(),
                    invoiceId = invoice.id,
                    productId = detail.productId,
                    quantity = detail.quantity,
                    unitPrice = product.price
                };
                _context.InvoiceDetails.Add(invoiceDetail);
            }

            await _context.SaveChangesAsync();
            return invoice;
        }
    }
}