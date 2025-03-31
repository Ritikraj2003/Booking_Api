using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Booking_Api.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        public DateTime Date { get; set; }
        public string Time { get; set; } 
        public string BookingType { get; set; } 
        public string CarCompany { get; set; } 
        public string CarModel { get; set; } 
        public string Pickup { get; set; } 
        public string Drop { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNo { get; set; } 
    }
}
