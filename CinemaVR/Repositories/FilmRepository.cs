using CinemaVR.Api.Data;
using CinemaVR.Api.Models;
using CinemaVR.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaVR.Api.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly CinemaContext _context;

        public FilmRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context.Films.ToListAsync();
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task AddAsync(Film film)
        {
            await _context.Films.AddAsync(film);
        }

        public Task UpdateAsync(Film film)
        {
            _context.Films.Update(film);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Film film)
        {
            _context.Films.Remove(film);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}