using API_Shop_Online.Dto.v1.Sale;
using API_Shop_Online.Services.Sales;
using Microsoft.AspNetCore.Mvc;

namespace API_Shop_Online.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSale([FromBody] SaleSubmissionDto dto)
        {
            var result = await _salesService.RegisterSaleAsync(dto);
            return CreatedAtAction(nameof(RegisterSale), new { id = result.Id }, result);
        }
    }
}
