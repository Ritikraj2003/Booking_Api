using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Booking_Api.Models;

namespace Booking_Api.Services
{
    public class ContactsService
    {
        private readonly IMongoCollection<Contacts> _Contactss;

        public ContactsService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _Contactss = database.GetCollection<Contacts>(settings.Value.CollectionName);
        }

        // Get All Bookings
        public async Task<List<Contacts>> GetAllAsync() =>
            await _Contactss.Find(_ => true).ToListAsync();

        // Get Booking by ID
        public async Task<Contacts?> GetByIdAsync(string id) =>
            await _Contactss.Find(b => b.Id == id).FirstOrDefaultAsync();

        // Create Booking
        public async Task<Contacts> CreateAsync(Contacts contact)
        {
            await _Contactss.InsertOneAsync(contact);
            return contact;
        }

        // Update Booking
        public async Task<bool> UpdateAsync(string id, Contacts updatedcontact)
        {
            var result = await _Contactss.ReplaceOneAsync(b => b.Id == id, updatedcontact);
            return result.ModifiedCount > 0;
        }

        // Delete Booking
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _Contactss.DeleteOneAsync(b => b.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
