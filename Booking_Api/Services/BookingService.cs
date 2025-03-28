using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Booking_Api.Models;

namespace Booking_Api.Services
{
    public class BookingService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _bookings = database.GetCollection<Booking>(settings.Value.CollectionName);
        }

        // Get All Bookings
        public async Task<List<Booking>> GetAllAsync() =>
            await _bookings.Find(_ => true).ToListAsync();

        // Get Booking by ID
        public async Task<Booking?> GetByIdAsync(string id) =>
            await _bookings.Find(b => b.Id == id).FirstOrDefaultAsync();

        // Create Booking
        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _bookings.InsertOneAsync(booking);
            return booking;
        }

        // Update Booking
        public async Task<bool> UpdateAsync(string id, Booking updatedBooking)
        {
            var result = await _bookings.ReplaceOneAsync(b => b.Id == id, updatedBooking);
            return result.ModifiedCount > 0;
        }

        // Delete Booking
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _bookings.DeleteOneAsync(b => b.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
