using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Repositories;
using MovieAPI.Enums;
using Xunit;
using MovieAPI.Persistence;
using Microsoft.Extensions.Logging.Abstractions;

namespace MovieAPI.Tests.Repositories
{
    public class MovieRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Movies.AddRange(
                new Movie { Title = "The Batman", Genre = "Action, Crime", Release_Date = new DateTime(2022, 3, 1), Overview = "Dark knight returns", Original_Language = "en", Poster_Url = "http://example.com/batman.jpg" },
                new Movie { Title = "Batman Begins", Genre = "Action", Release_Date = new DateTime(2005, 6, 15), Overview = "Bruce Wayne starts his journey", Original_Language = "en", Poster_Url = "http://example.com/batmanbegins.jpg" },
                new Movie { Title = "Spider-Man", Genre = "Action, Adventure", Release_Date = new DateTime(2002, 5, 3), Overview = "Peter Parker becomes Spider-Man", Original_Language = "en", Poster_Url = "http://example.com/spiderman.jpg" },
                new Movie { Title = "The Flash", Genre = "Action, Sci-Fi", Release_Date = new DateTime(2023, 6, 23), Overview = "Barry Allen manipulates time", Original_Language = "en", Poster_Url = "http://example.com/flash.jpg" },
                new Movie { Title = "Zebra Man", Genre = "Drama", Release_Date = new DateTime(2021, 1, 1), Overview = "A strange hero emerges", Original_Language = "en", Poster_Url = "http://example.com/zebraman.jpg" }
            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Search_By_Title_Returns_Correct_Movies()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { Title = "batman" });

            Assert.Equal(2, result.Count);
            Assert.All(result, m => Assert.Contains("batman", m.Title, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task Search_With_Pagination_Returns_Correct_Page()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { Page = 2, PageSize = 2 });

            Assert.Equal(2, result.Count);
            Assert.Equal("Spider-Man", result[0].Title);
        }

        [Fact]
        public async Task Search_With_PageSize_Limits_Results()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { PageSize = 3 });

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Filter_By_Genre_Returns_Matching_Movies()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { Genre = "Adventure" });

            Assert.True(result.Any());
            Assert.All(result, m => Assert.Contains("Adventure", m.Genre, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task Filter_By_Year_Returns_Matching_Movies()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { Year = 2023 });

            Assert.True(result.Any());
            Assert.Contains(result, m => m.Title == "The Flash");
        }

        [Fact]
        public async Task Sort_By_Title_Descending_Returns_Z_First()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { SortBy = SortBy.Title, SortDirection = SortDirection.Desc });

            Assert.Equal("Zebra Man", result.First().Title);
        }

        [Fact]
        public async Task Sort_By_ReleaseDate_Ascending_Returns_Earliest_First()
        {
            var repo = CreateRepository();

            var result = await repo.SearchAsync(new MovieSearchRequest { SortBy = SortBy.Year, SortDirection = SortDirection.Asc });

            Assert.Equal("Spider-Man", result.First().Title);
        }

        private MovieRepository CreateRepository()
        {
            var context = GetInMemoryDbContext();
            var repo = new MovieRepository(context, NullLogger<MovieRepository>.Instance);
            return repo;
        }
    }
}
