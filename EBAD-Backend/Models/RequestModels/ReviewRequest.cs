using System.ComponentModel.DataAnnotations;

namespace EBAD_Backend.Models.RequestModels
{
    public class ReviewRequest
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public string ProductCategory { get; set; }

        [Required]
        public string ReviewerName { get; set; }

        [Required]
        public string ReviewerEmailAddress { get; set; }

        [Required]
        public string ReviewContent { get; set; }

        [Required]
        public string ProductImage { get; set; }
    }
}
