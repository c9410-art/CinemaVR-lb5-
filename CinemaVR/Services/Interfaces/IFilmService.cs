using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;

namespace CinemaVR.Api.Services.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllAsync(string? genre);
        Task<Film?> GetByIdAsync(int id);
        Task<Film> CreateAsync(FilmDto dto);
        Task UpdateAsync(int id, FilmDto dto);
        Task DeleteAsync(int id);
    }
}