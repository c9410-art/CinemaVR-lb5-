using CinemaVR.Api.Models;

namespace CinemaVR.Api.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetAllAsync();

        Task<Film?> GetByIdAsync(int id);

        Task AddAsync(Film film);

        Task UpdateAsync(Film film);

        Task DeleteAsync(Film film);

        Task SaveAsync();
    }
}