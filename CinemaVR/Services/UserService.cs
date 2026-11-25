using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using CinemaVR.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaVR.Api.Services
{
    public class UserService : IUserService
    {
        private readonly CinemaContext _context;

        public UserService(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new Exception("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}