using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using CinemaVR.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaVR.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly CinemaContext _context;

        public BookingService(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking> CreateAsync(BookingDto dto)
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

            return booking;
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                throw new Exception("Booking not found");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}