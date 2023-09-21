using EBAD_Backend.Models.RequestModels;
using EBAD_Backend.Models.ResponseModels;

namespace EBAD_Backend.Services.Interface
{
    public interface IPurchaseService
    {
        Task<BaseResponse<bool>> InsertProduct(ProductRequest request);
        Task<BaseResponse<bool>> InsertPurchase(PurchaseRequest request);
    }
}
