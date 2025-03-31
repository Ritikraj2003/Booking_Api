using Booking_Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BookingService
{
    private readonly IMongoCollection<Booking> _bookingCollection;

    public BookingService(IOptions<MongoDbSettings> bookingsetting)
    {
        var client = new MongoClient(bookingsetting.Value.ConnectionString);
        var database = client.GetDatabase(bookingsetting.Value.DatabaseName); 
        _bookingCollection = database.GetCollection<Booking>("booking");
    }

    public async Task<List<Booking>> GetAllAsync() =>
        await _bookingCollection.Find(_ => true).ToListAsync();

    public async Task<Booking> GetByIdAsync(string id) =>
        await _bookingCollection.Find(b => b.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Booking booking) =>
        await _bookingCollection.InsertOneAsync(booking);

    public async Task UpdateAsync(string id, Booking booking) =>
        await _bookingCollection.ReplaceOneAsync(b => b.Id == id, booking);

    public async Task DeleteAsync(string id) =>
        await _bookingCollection.DeleteOneAsync(b => b.Id == id);
}
