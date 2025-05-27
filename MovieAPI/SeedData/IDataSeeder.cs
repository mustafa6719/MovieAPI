namespace MovieAPI.SeedData
{
    public interface IDataSeeder
    {
        void SeedMoviesFromCsv(string path);
    }
}
