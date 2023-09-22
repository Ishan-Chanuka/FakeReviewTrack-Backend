
namespace EBAD_Backend.Models.RequestModels
{
    public class ReviewRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
        public string ReviewerEmailAddress { get; set; } = string.Empty;
        public string ReviewContent { get; set; } = string.Empty;
        public byte[] ProductImage { get; set; }
    }
}
