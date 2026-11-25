using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;

namespace CinemaVR.Api.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(int id);
        Task<Session> CreateAsync(SessionDto dto);
        Task UpdateAsync(int id, SessionDto dto);
        Task DeleteAsync(int id);
    }
}