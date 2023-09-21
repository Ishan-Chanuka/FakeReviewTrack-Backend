
namespace EBAD_Backend.Models.RequestModels
{
    public class ReviewRequest
    {
        public string ProductId { get; set; }
        public string OrderId { get; set; }
        public string ProductCategory { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmailAddress { get; set; }
        public string ReviewContent { get; set; }
    }
}
