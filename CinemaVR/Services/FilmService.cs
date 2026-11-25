using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using CinemaVR.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaVR.Api.Services
{
    public class FilmService : IFilmService
    {
        private readonly CinemaContext _context;

        public FilmService(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Film>> GetAllAsync(string? genre)
        {
            var query = _context.Films.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(f => f.Genre == genre);

            return await query.ToListAsync();
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<Film> CreateAsync(FilmDto dto)
        {
            var film = new Film
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Price = dto.Price,
                Year = dto.Year
            };

            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();

            return film;
        }

        public async Task UpdateAsync(int id, FilmDto dto)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                throw new Exception("Film not found");

            film.Title = dto.Title;
            film.Genre = dto.Genre;
            film.Price = dto.Price;
            film.Year = dto.Year;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                throw new Exception("Film not found");

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
        }
    }
}