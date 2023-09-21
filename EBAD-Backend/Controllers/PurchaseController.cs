using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EBAD_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost("add-purchase")]
        public async Task<IActionResult> AddPurchase([FromBody] PurchaseRequest purchase)
        {
            var result = await _purchaseService.InsertPurchase(purchase);
            return Ok(result);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest product)
        {
            var result = await _purchaseService.InsertProduct(product);
            return Ok(result);
        }
    }
}
