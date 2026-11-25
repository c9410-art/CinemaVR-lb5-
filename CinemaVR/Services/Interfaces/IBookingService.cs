using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;

namespace CinemaVR.Api.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> CreateAsync(BookingDto dto);
        Task DeleteAsync(int id);
    }
}