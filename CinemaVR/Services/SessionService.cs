using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using CinemaVR.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaVR.Api.Services
{
    public class SessionService : ISessionService
    {
        private readonly CinemaContext _context;

        public SessionService(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task<Session> CreateAsync(SessionDto dto)
        {
            var session = new Session
            {
                FilmId = dto.FilmId,
                Date = dto.Date,
                Hall = dto.Hall
            };

            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return session;
        }

        public async Task UpdateAsync(int id, SessionDto dto)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                throw new Exception("Session not found");

            session.FilmId = dto.FilmId;
            session.Date = dto.Date;
            session.Hall = dto.Hall;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                throw new Exception("Session not found");

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}