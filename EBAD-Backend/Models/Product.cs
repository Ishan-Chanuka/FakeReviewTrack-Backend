using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EBAD_Backend.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("pruductname")]
        public string ProductName { get; set; }

        [BsonElement("discription")]
        public string ProductDescription { get; set; }

        [BsonElement("category")]
        public string ProductCategory { get; set; }

        [BsonElement("price")]
        public string ProductPrice { get; set; }
    }
}
