using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Booking_Api.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        [BsonElement("Company")]
        public string Company { get; set; }

        [BsonElement("models")]
        public List<string> CarModels { get; set; } = new List<string>();
    }
}
