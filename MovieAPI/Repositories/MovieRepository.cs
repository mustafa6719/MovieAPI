using Microsoft.EntityFrameworkCore;
using MovieAPI.Controllers;
using MovieAPI.Enums;
using MovieAPI.Models;
using MovieAPI.Persistence;
using System.Linq;

namespace MovieAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieRepository> _logger;
        public MovieRepository(AppDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Movie>> SearchAsync(MovieSearchRequest movieSearchRequest)
        {
            var query = _context.Movies.AsQueryable();
            // Filtering
            if (!string.IsNullOrWhiteSpace(movieSearchRequest.Title))
                query = query.Where(m => m.Title.ToLower().Contains(movieSearchRequest.Title.ToLower()));

            if (!string.IsNullOrWhiteSpace(movieSearchRequest.Genre))
                query = query.Where(m => m.Genre.ToLower().Contains(movieSearchRequest.Genre.ToLower()));

            if (movieSearchRequest.Year.HasValue)
                query = query.Where(m => m.Release_Date.Year == movieSearchRequest.Year);

            // Sorting
            if (movieSearchRequest.SortBy == SortBy.Title)
                query = movieSearchRequest.SortDirection == SortDirection.Desc
                    ? query.OrderByDescending(m => m.Title)
                    : query.OrderBy(m => m.Title);

            else if (movieSearchRequest.SortBy == SortBy.Year)
                query = movieSearchRequest.SortDirection == SortDirection.Desc
                    ? query.OrderByDescending(m => m.Release_Date)
                    : query.OrderBy(m => m.Release_Date);

            // Pagination
            query = query
                   .Skip((movieSearchRequest.Page - 1) * movieSearchRequest.PageSize)
                   .Take(movieSearchRequest.PageSize);
            
            var result = await query.ToListAsync();
            _logger.LogInformation($"SearchAsync returned {result.Count} movie(s).");

            return result;
        }
    }
}

