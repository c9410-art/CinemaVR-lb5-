using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaVR.Api.Data;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;

namespace CinemaVR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public SessionsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetAll()
        {
            return Ok(await _context.Sessions.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetById(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                return NotFound();

            return Ok(session);
        }

        [HttpPost]
        public async Task<ActionResult<Session>> Create(SessionDto dto)
        {
            var session = new Session
            {
                FilmId = dto.FilmId,
                Date = dto.Date,
                Hall = dto.Hall
            };

            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = session.Id },
                session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            SessionDto dto)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                return NotFound();

            session.FilmId = dto.FilmId;
            session.Date = dto.Date;
            session.Hall = dto.Hall;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                return NotFound();

            _context.Sessions.Remove(session);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}