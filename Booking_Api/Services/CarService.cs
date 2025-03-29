using Booking_Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Booking_Api.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _carsCollection;

        public CarService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _carsCollection = database.GetCollection<Car>("Cars");
        }

        public async Task<List<Car>> GetAllAsync() =>
            await _carsCollection.Find(_ => true).ToListAsync();

        public async Task<Car?> GetByIdAsync(string id) => 
            await _carsCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Car car) => 
            await _carsCollection.InsertOneAsync(car);

        public async Task UpdateAsync(string id, Car car) =>
            await _carsCollection.ReplaceOneAsync(c => c.Id == id, car);

        public async Task DeleteAsync(string id) => 
            await _carsCollection.DeleteOneAsync(c => c.Id == id);
    }
}
