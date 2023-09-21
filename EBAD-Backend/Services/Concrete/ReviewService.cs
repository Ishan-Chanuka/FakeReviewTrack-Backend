using EBAD_Backend.DataAccess.Interface;
using EBAD_Backend.DataAccess;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using EBAD_Backend.Models;
using EBAD_Backend.Services.Interface;
using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Models.ResponseModels;
using EBAD_Backend.Exceptions;
using System.Net;

namespace EBAD_Backend.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IDataAccess _dataAccess;
        private readonly MongoSettings _settings;
        private readonly IMongoCollection<Review> _review;

        public ReviewService(IDataAccess data, IOptions<MongoSettings> options)
        {
            _dataAccess = data;
            _settings = options.Value;
            _review = _dataAccess.ConnectToMongo<Review>(_settings.ReviewsCollection);
        }

        public async Task<BaseResponse<bool>> InsertReview(ReviewRequest request)
        {
            var existsInDb = _review.Find(r =>
                r.OrderId == request.OrderId &&
                r.ReviewerName == request.ReviewerName &&
                r.ReviewerEmailAddress == request.ReviewerEmailAddress).Any();

            if (existsInDb)
            {
                throw new ApiException("Review already exists")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            #region Fake Review Check

            // check fake review here
            //var fake = true;

            //if (fake)
            //{
            //    return new BaseResponse<bool>()
            //    {
            //        Message = "Review is fake or computer gernerated",
            //        Success = false,
            //    };
            //}

            #endregion

            var review = new Review
            {
                ProductId = request.ProductId,
                OrderId = request.OrderId,
                ProductCategory = request.ProductCategory,
                ReviewerName = request.ReviewerName,
                ReviewerEmailAddress = request.ReviewerEmailAddress,
                ReviewContent = request.ReviewContent,
            };

            try
            {
                await _review.InsertOneAsync(review);
                return new BaseResponse<bool>()
                {
                    Message = "Review added successfully",
                    Success = true,
                };
            }
            catch(Exception e)
            {
                throw new ApiException($"Failed to add review. \n{e.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IList<Review>>> GetAllReviewByProductId(string productId)
        {
            var reviews = await _review.FindAsync(r => r.ProductId == productId);

            return new BaseResponse<IList<Review>>()
            {
                Message = "Reviews fetched successfully",
                Success = true,
                Data = reviews.ToList()
            };
        }

    }
}
