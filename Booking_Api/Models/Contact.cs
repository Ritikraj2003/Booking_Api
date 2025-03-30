using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Booking_Api.Models
{
    public class Contacts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("phoneNo")]
        public string PhoneNo { get; set; } 

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public string Address { get; set; } = null!;
    }
}
