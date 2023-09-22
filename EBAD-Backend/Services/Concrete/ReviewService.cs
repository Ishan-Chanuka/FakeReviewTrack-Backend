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
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System.Text;

namespace EBAD_Backend.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IDataAccess _dataAccess;
        private readonly MongoSettings _settings;
        private readonly IMongoCollection<Review> _review;
        private readonly IMongoCollection<Purchase> _purchase;

        public ReviewService(IDataAccess data, IOptions<MongoSettings> options)
        {
            _dataAccess = data;
            _settings = options.Value;
            _review = _dataAccess.ConnectToMongo<Review>(_settings.ReviewsCollection);
            _purchase = _dataAccess.ConnectToMongo<Purchase>(_settings.PurchasesCollection);
        }

        public async Task<BaseResponse<bool>> UserVerification(string productId, string name, string email, string orderId)
        {
            var purchaseExist = await _purchase.Find(p => 
                p.OrderId == orderId && 
                p.CustomerEmailAddress == email).AnyAsync();

            var reviewExist = await _review.Find(r =>
                r.ProductId == productId &&
                r.OrderId == orderId &&
                r.ReviewerEmailAddress == email).AnyAsync();

            if (purchaseExist && reviewExist)
            {
                return new BaseResponse<bool>()
                {
                    Message = "You already reviewed this product",
                    Success = true,
                };
            }

            return new BaseResponse<bool>()
            {
                Message = "You can review this product",
                Success = false,
            };
        }

        public async Task<BaseResponse<IList<Review>>> InsertReview(ReviewRequest request)
        {
            if (request is null || string.IsNullOrEmpty(request.ReviewContent) || string.IsNullOrWhiteSpace(request.ReviewContent))
            {
                throw new ApiException("Request cannot be null")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            using var client = new HttpClient();
            try
            {
                var apirequest = new HttpRequestMessage(HttpMethod.Post, "https://fake-review-ml.azurewebsites.net/predict");

                var requestBody = new
                {
                    review = request.ReviewContent
                };

                var jsonBody = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(jsonBody, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                apirequest.Content = content;

                HttpResponseMessage response = await client.SendAsync(apirequest);


                var isReviewd = await UserVerification(request.ProductId, request.ReviewerName, request.ReviewerEmailAddress, request.OrderId);

                if (response.IsSuccessStatusCode)
                {
                    string predictionResponse = await response.Content.ReadAsStringAsync();
                    dynamic predictionObject = JsonConvert.DeserializeObject(predictionResponse)!;
                    string prediction = (string)predictionObject.prediction;

                    if (prediction == "CG")
                    {
                        return new BaseResponse<IList<Review>>()
                        {
                            Message = "This review is likely suspicious or computer gernerated!",
                            Success = false,
                            Data = new List<Review>()
                        };
                    }

                    else if (prediction == "OR" && isReviewd.Success == false)
                    {
                        var review = new Review
                        {
                            ProductId = request.ProductId,
                            OrderId = request.OrderId,
                            ProductCategory = request.ProductCategory,
                            ReviewerName = request.ReviewerName,
                            ReviewerEmailAddress = request.ReviewerEmailAddress,
                            ReviewContent = request.ReviewContent,
                            ProductImage = request.ProductImage
                        };

                        await _review.InsertOneAsync(review);

                        var reviewList = GetAllReviewByProductId(request.ProductId);

                        return new BaseResponse<IList<Review>>()
                        {
                            Message = "Review added successfully",
                            Success = true,
                            Data = reviewList.Result.Data
                        };
                    }

                    return new BaseResponse<IList<Review>>()
                    {
                        Message = "Unabled to added review due to " + ((prediction == "CG") ?
                                  "review is fake" : (isReviewd.Success == true) ?
                                  "user is already reviewd this product" : "something went wrong"),
                        Success = false,
                        Data = new List<Review>()
                    };
                }

                return new BaseResponse<IList<Review>>()
                {
                    Message = "Unabled to added review due to " + ((response.StatusCode == HttpStatusCode.BadRequest) ?
                              "bad request" : (response.StatusCode == HttpStatusCode.InternalServerError) ?
                              "internal server error" : "something went wrong"),
                    Success = false,
                    Data = new List<Review>()
                };

            }
            catch (Exception e)
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

            if (reviews == null)
            {
                return new BaseResponse<IList<Review>>()
                {
                    Message = "No reviews found",
                    Success = false,
                    Data = new List<Review>()
                };
            }

            return new BaseResponse<IList<Review>>()
            {
                Message = "Reviews fetched successfully",
                Success = true,
                Data = reviews.ToList()
            };
        }

    }
}
