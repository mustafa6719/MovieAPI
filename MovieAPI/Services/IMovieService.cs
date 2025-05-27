using MovieAPI.Models;

namespace MovieAPI.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> SearchMoviesAsync(MovieSearchRequest movieSearchRequest);
    }
}
