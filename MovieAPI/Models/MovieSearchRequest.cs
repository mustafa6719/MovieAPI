using System.ComponentModel;
using MovieAPI.Enums;

namespace MovieAPI.Models
{
    /// <summary>
    /// Search criteria for filtering and sorting movies.
    /// </summary>
    public class MovieSearchRequest
    {
        /// <summary>The movie title or part of it.</summary>
        [DefaultValue("batman")]
        public string? Title { get; set; }

        /// <summary>Movie genre (e.g., Action, Drama).</summary>
        [DefaultValue("Crime")]
        public string? Genre { get; set; }

        /// <summary>Filter by release year (e.g., 2022).</summary>
        [DefaultValue(2022)]
        public int? Year { get; set; }

        /// <summary>Page number of results (default 1).</summary>
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>Number of results per page (default 10).</summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        /// <summary>Sort field.</summary>
        [DefaultValue(SortBy.Title)]
        public SortBy SortBy { get; set; }

        /// <summary>Sort direction.</summary>
        [DefaultValue(SortDirection.Asc)]
        public SortDirection SortDirection { get; set; }
    }
}
