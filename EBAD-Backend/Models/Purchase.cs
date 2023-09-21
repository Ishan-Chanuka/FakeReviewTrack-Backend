using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EBAD_Backend.Models
{
    public class Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PurchaseId { get; set; }

        [BsonElement("orderId")]
        public string OrderId { get; set; }

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("name")]
        public string CustomerName { get; set; }

        [BsonElement("email")]
        public string CustomerEmailAddress { get; set; }

        [BsonElement("phone")]
        public string CustomerPhoneNumber { get; set; }

        [BsonElement("image")]
        public string ProductImage { get; set; }

        [BsonElement("date")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
