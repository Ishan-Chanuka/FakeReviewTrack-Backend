using EBAD_Backend.Models;
using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Models.ResponseModels;

namespace EBAD_Backend.Services.Interface
{
    public interface IReviewService
    {
        Task<BaseResponse<bool>> InsertReview(ReviewRequest request);
        Task<BaseResponse<IList<Review>>> GetAllReviewByProductId(string productId);
    }
}
