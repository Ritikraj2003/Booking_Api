using Booking_Api.Models;
using Booking_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly EmailService _emailService;
        public BookingController(BookingService bookingService, EmailService emailService)
        {
            _bookingService = bookingService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Create(Booking booking)
        {
            await _bookingService.CreateAsync(booking);
            // Send Email Notification
            string subject = "New Booking Created";
            string body = $"A new booking has been made by {booking.Name}.\n\n" +
                          $"Details:\nName: {booking.Name}\nEmail: {booking.Email}\nPhone: {booking.PhoneNo}\n" +
                          $"CarModel: {booking.CarModel}\nDate: {booking.Date}\n" +
                          $"DropLocation:{booking.Drop}\n PickupLocation:{booking.Pickup}\n Time:{booking.Time}";

            await _emailService.SendEmailAsync("mandeepkumar8521@gmail.com", subject, body); // Sending email

            return Ok();
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Booking booking)
        {
            var existingBooking = await _bookingService.GetByIdAsync(id);
            if (existingBooking == null) return NotFound();

            booking.Id = id;
            await _bookingService.UpdateAsync(id, booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingBooking = await _bookingService.GetByIdAsync(id);
            if (existingBooking == null) return NotFound();

            await _bookingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
