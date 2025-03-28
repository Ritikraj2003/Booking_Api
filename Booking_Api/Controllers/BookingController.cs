using Microsoft.AspNetCore.Mvc;
using Booking_Api.Models;
using Booking_Api.Services;

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

        // Get All Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Get()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        // Get Booking by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
                return NotFound("Booking not found");

            return Ok(booking);
        }

        // Create Booking + Send Email
        //[HttpPost]
        //public async Task<ActionResult<Booking>> Create(Booking booking)
        //{
        //    var createdBooking = await _bookingService.CreateAsync(booking);

        //    // Send Email Notification
        //    string subject = "New Booking Created";
        //    string body = $"A new booking has been made by {booking.Name}.\n\n" +
        //                  $"Details:\nName: {booking.Name}\nAge: {booking.Age}\nPhone: {booking.PhoneNo}\n" +
        //                  $"Email: {booking.Email}\nAddress: {booking.Address}";

        //    await _emailService.SendEmailAsync(booking.Email, subject, body);

        //    return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
        //}
        [HttpPost]
        public async Task<ActionResult<Booking>> Create(Booking booking)
        {
            // ✅ Ensure Email is Not Null
            if (string.IsNullOrWhiteSpace(booking.Email))
            {
                return BadRequest("Email is required.");
            }

            var createdBooking = await _bookingService.CreateAsync(booking);

            // Send Email Notification
            string subject = "New Booking Created";
            string body = $"A new booking has been made by {booking.Name}.\n\n" +
                          $"Details:\nName: {booking.Name}\nAge: {booking.Age}\nPhone: {booking.PhoneNo}\n" +
                          $"Email: {booking.Email}\nAddress: {booking.Address}";

            await _emailService.SendEmailAsync("ritikraj1092002@gmail.com", subject, body); // Sending email

            return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
        }


        // Update Booking
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Booking updatedBooking)
        {
            var existingBooking = await _bookingService.GetByIdAsync(id);
            if (existingBooking == null)
                return NotFound("Booking not found");

            updatedBooking.Id = id; // Ensure ID remains the same
            bool updated = await _bookingService.UpdateAsync(id, updatedBooking);
            if (!updated)
                return BadRequest("Update failed");

            return NoContent();
        }

        // Delete Booking
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingBooking = await _bookingService.GetByIdAsync(id);
            if (existingBooking == null)
                return NotFound("Booking not found");

            bool deleted = await _bookingService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Delete failed");

            return NoContent();
        }
    }
}
