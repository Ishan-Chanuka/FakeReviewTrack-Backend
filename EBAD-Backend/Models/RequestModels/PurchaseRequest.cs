namespace EBAD_Backend.Models.RequestModels
{
    public class PurchaseRequest
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string ProductImage { get; set; }
    }
}
