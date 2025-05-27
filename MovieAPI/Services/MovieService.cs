using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<Movie>> SearchMoviesAsync(MovieSearchRequest movieSearchRequest)
        {
            return await _movieRepository.SearchAsync(movieSearchRequest);
        }
    }

}
