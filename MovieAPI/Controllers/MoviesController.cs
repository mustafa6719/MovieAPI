using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;
using Swashbuckle.AspNetCore.Filters;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;
        public MoviesController(IMovieService movieService,ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }
        /// <summary>
        /// Searches movies based on title, genre, year, pagination and sorting.
        /// </summary>
        /// <param name="movieSearchRequest">Search filters including title, genre, year, page, and sorting.</param>
        /// <returns>A paginated list of matching movies.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]MovieSearchRequest movieSearchRequest)
        {
            try
            {
                _logger.LogInformation($"Search called with Title='{movieSearchRequest.Title}', Genre='{movieSearchRequest.Genre}', Year={movieSearchRequest.Year}, Page={movieSearchRequest.Page}, " +
                                       $"PageSize={movieSearchRequest.PageSize}, SortBy={movieSearchRequest.SortBy}, SortDir={movieSearchRequest.SortDirection}"
    );
                var results = await _movieService.SearchMoviesAsync(movieSearchRequest);
                return Ok(results);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Search failed with an unexpected error.");
                return BadRequest("An unexpected error occurred.");
            }
        }

    }
}
