using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EBAD_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest review)
        {
            var result = await _reviewService.InsertReview(review);
            return Ok(result);
        }

        [HttpGet("get-all-review-by-product-id/{productId}")]
        public async Task<IActionResult> GetAllReviewByProductId(string productId)
        {
            var result = await _reviewService.GetAllReviewByProductId(productId);
            return Ok(result);
        }

        [HttpGet("user-verification/{productId}/{name}/{email}")]
        public async Task<IActionResult> UserVerification(string productId, string name, string email)
        {
            var result = await _reviewService.UserVerification(productId, name, email);
            return Ok(result);
        }
    }
}
