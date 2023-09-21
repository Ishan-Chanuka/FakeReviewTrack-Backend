using EBAD_Backend.DataAccess;
using EBAD_Backend.DataAccess.Interface;
using EBAD_Backend.Exceptions;
using EBAD_Backend.Models;
using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Models.ResponseModels;
using EBAD_Backend.Services.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net;

namespace EBAD_Backend.Services.Concrete
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IDataAccess _dataAccess;
        private readonly MongoSettings _settings;
        private readonly IMongoCollection<Purchase> _purchase;
        private readonly IMongoCollection<Product> _product;

        public PurchaseService(IDataAccess data, IOptions<MongoSettings> options)
        {
            _dataAccess = data;
            _settings = options.Value;
            _purchase = _dataAccess.ConnectToMongo<Purchase>(_settings.PurchasesCollection);
            _product = _dataAccess.ConnectToMongo<Product>(_settings.ProductsCollection);
        }

        public async Task<BaseResponse<bool>> InsertProduct(ProductRequest request)
        {
            try
            {
                var product = new Product
                {
                    ProductName = request.ProductName,
                    ProductDescription = request.ProductDescription,
                    ProductCategory = request.ProductCategory,
                    ProductPrice = request.ProductPrice,
                };

                await _product.InsertOneAsync(product);

                return new BaseResponse<bool>()
                {
                    Message = "Product added successfully",
                    Success = true
                };
            }
            catch(Exception ex)
            {
                throw new ApiException($"Error adding product. \n{ex.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> InsertPurchase(PurchaseRequest request)
        {
            try
            {
                var purchase = new Purchase
                {
                    OrderId = request.OrderId,
                    ProductId = request.ProductId,
                    CustomerName = request.CustomerName,
                    CustomerEmailAddress = request.CustomerEmailAddress,
                    CustomerPhoneNumber = request.CustomerPhoneNumber,
                    ProductImage = request.ProductImage,
                };

                await _purchase.InsertOneAsync(purchase);

                return new BaseResponse<bool>()
                {
                    Message = "Purchase added successfully",
                    Success = true
                };
            }
            catch(Exception ex)
            {
                throw new ApiException($"Error adding purchase. \n{ex.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
