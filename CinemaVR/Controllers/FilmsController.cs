using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using CinemaVR.Api.Services.Interfaces;

namespace CinemaVR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAll(
            [FromQuery] string? genre)
        {
            return Ok(await _filmService.GetAllAsync(genre));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetById(int id)
        {
            var film = await _filmService.GetByIdAsync(id);

            if (film == null)
                return NotFound();

            return Ok(film);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Film>> Create(FilmDto dto)
        {
            var film = await _filmService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = film.Id },
                film);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            FilmDto dto)
        {
            await _filmService.UpdateAsync(id, dto);

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _filmService.DeleteAsync(id);

            return NoContent();
        }
    }
}