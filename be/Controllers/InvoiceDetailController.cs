using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be.Data;
using be.Models;
using be.DTOs;
using be.Services.Interfaces;

namespace be.Controllers
{
    [Route("api/invoice-details")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceDetailService;

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
        {
            _invoiceDetailService = invoiceDetailService;
        }

        [HttpGet("invoiceDetails/{invoiceId}/invoiceId")]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetInvoiceDetailByInvoiceId(string invoiceId)
        {
            var invoiceDetails = await _invoiceDetailService.GetInvoiceDetailByInvoiceIdAsync(invoiceId);

            return Ok(invoiceDetails);
        }

    }
}