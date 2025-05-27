using MovieAPI.Models;

namespace MovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> SearchAsync(MovieSearchRequest movieSearchRequest);
    }
}
