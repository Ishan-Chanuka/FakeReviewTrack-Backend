using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EBAD_Backend.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; }

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("orderId")]
        public string OrderId { get; set; }

        [BsonElement("category")]
        public string ProductCategory { get; set; }

        [BsonElement("name")]
        public string ReviewerName { get; set; }

        [BsonElement("email")]
        public string ReviewerEmailAddress { get; set; }

        [BsonElement("phone")]
        public string ReviewContent { get; set; }

        [BsonElement("image")]
        public string ProductImage { get; set; }

        [BsonElement("date")]
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
