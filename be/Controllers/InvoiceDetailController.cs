using Microsoft.AspNetCore.Mvc;
using be.Models;
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