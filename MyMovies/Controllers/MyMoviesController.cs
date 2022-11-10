using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovieAPI.Models;
using MyMovieApi.Models;
using Microsoft.AspNetCore.Cors;
using MakingHttpRequest;

namespace MyMovies.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MyMoviesController : ControllerBase
    {
        private readonly MyMovieContext _context;
        private readonly IExternalMovies _externalMovies;

        public MyMoviesController(MyMovieContext context, IExternalMovies externalMovies)
        {
            _context = context;
            _externalMovies = externalMovies;
        }

        // GET: api/MyMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyMovieDTO>>> GetMyMovies()
        {
            return await _context.MyMovies
                .Select(x => MovieDTO(x))
                .ToListAsync();
        }

        // GET: api/MyMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyMovieDTO>> GetMyMovie(long id)
        {
            var myMovie = await _context.MyMovies.FindAsync(id);

            if (myMovie == null)
            {
                return NotFound();
            }

            return MovieDTO(myMovie);
        }

        [HttpGet("download")]
        public async Task<string> Get()
        {
            return await _externalMovies.Get();
        }

        // PUT: api/MyMovies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyMovie(long id, MyMovieDTO myMovieDTO)
        {
            if (id != myMovieDTO.Id)
            {
                return BadRequest();
            }

            var movie = await _context.MyMovies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = myMovieDTO.Title;
            movie.Director = myMovieDTO.Director;
            movie.Year = myMovieDTO.Year;
            movie.Rate = myMovieDTO.Rate;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyMovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MyMovies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyMovieDTO>> PostMyMovie(MyMovieDTO myMovieDTO)
        {
            var movie = new MyMovie
            {
                Title = myMovieDTO.Title,
                Director = myMovieDTO.Director,
                Year = myMovieDTO.Year,
                Rate = myMovieDTO.Rate
            };

            _context.MyMovies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyMovie), new { id = movie.Id }, MovieDTO(movie));
        }

        // DELETE: api/MyMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyMovie(long id)
        {
            var movie = await _context.MyMovies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.MyMovies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyMovieExists(long id)
        {
            return (_context.MyMovies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       private static MyMovieDTO MovieDTO(MyMovie movie) =>
           new MyMovieDTO
           {
               Id = movie.Id,
               Title = movie.Title,
               Director = movie.Director,
               Year = movie.Year,
               Rate = movie.Rate
           };
    }
}
