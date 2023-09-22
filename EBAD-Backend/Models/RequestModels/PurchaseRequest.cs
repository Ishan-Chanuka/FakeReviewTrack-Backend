namespace EBAD_Backend.Models.RequestModels
{
    public class PurchaseRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmailAddress { get; set; } = string.Empty;
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
    }
}
