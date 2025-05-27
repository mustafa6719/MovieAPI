using CsvHelper.Configuration.Attributes;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Ignore]
        public int Id { get; set; }
        public DateTime Release_Date { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public int Vote_Count { get; set; }
        public double Vote_Average { get; set; }
        public string Original_Language { get; set; }
        public string Genre { get; set; }
        public string Poster_Url { get; set; }
    }
}
