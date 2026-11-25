using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;

namespace CinemaVR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public BookingsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
            return Ok(await _context.Bookings.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Create(
            BookingDto dto)
        {
            var booking = new Booking
            {
                SessionId = dto.SessionId,
                UserId = dto.UserId,
                Seat = dto.Seat,
                Price = 150
            };

            await _context.Bookings.AddAsync(booking);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = booking.Id },
                booking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}