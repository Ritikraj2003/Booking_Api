using Booking_Api.Models;
using Booking_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAll() => Ok(await _carService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(string id)
        {
            var car = await _carService.GetByIdAsync(id);
            return car is not null ? Ok(car) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Car car)
        {
            await _carService.CreateAsync(car);
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Car car)
        {
            var existingCar = await _carService.GetByIdAsync(id);
            if (existingCar is null) return NotFound();

            car.Id = id; // Ensure ID remains the same
            await _carService.UpdateAsync(id, car);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingCar = await _carService.GetByIdAsync(id);
            if (existingCar is null) return NotFound();

            await _carService.DeleteAsync(id);
            return NoContent();
        }
    }
}
