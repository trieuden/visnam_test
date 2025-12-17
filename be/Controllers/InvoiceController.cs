using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be.Data;
using be.Models;
using be.DTOs;
using be.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using be.Hubs;

namespace be.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IHubContext<OrderHub> _hubContext;

        public InvoiceController(IInvoiceService invoiceService, IHubContext<OrderHub> hubContext)
        {
            _invoiceService = invoiceService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>?>> GetInvoices()
        {
            return await _invoiceService.GetInvoicesAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByUserId(string userId)
        {
            var invoices = await _invoiceService.GetInvoicesByUserIdAsync(userId);
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<ActionResult<Invoice?>> CreateInvoice(InvoiceCreateDto request)
        {
            var newInvoice = await _invoiceService.CreateInvoiceAsync(request);

            if (newInvoice != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNewOrder", newInvoice);
            }

            return Ok(newInvoice);
        }
    }
}