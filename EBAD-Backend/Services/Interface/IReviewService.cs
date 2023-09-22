using EBAD_Backend.Models;
using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Models.ResponseModels;

namespace EBAD_Backend.Services.Interface
{
    public interface IReviewService
    {
        Task<BaseResponse<IList<Review>>> InsertReview(ReviewRequest request);
        Task<BaseResponse<IList<Review>>> GetAllReviewByProductId(string productId);
        Task<BaseResponse<bool>> UserVerification(string productId, string name, string email);
    }
}
