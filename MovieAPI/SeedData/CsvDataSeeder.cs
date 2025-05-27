using CsvHelper;
using CsvHelper.Configuration;
using MovieAPI.Models;
using MovieAPI.Persistence;
using System.Globalization;

namespace MovieAPI.SeedData
{
    public class CsvDataSeeder : IDataSeeder
    {
        private readonly AppDbContext _context;

        public CsvDataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void SeedMoviesFromCsv(string path)
        {
            if (_context.Movies.Any()) return;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                ShouldSkipRecord = record =>
                {
                    // Skip records that are clearly missing critical data
                    return record.Row.Parser.Record.Length < 9;
                }
            };


            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            var movies = csv.GetRecords<Movie>()
                            .Where(m => !string.IsNullOrWhiteSpace(m.Title)) 
                            .ToList();

            _context.Movies.AddRange(movies);
            _context.SaveChanges();
        }
    }
}
