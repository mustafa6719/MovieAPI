using Microsoft.EntityFrameworkCore;
using MovieAPI.Persistence;
using MovieAPI.Repositories;
using MovieAPI.SeedData;
using MovieAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=movies.db"));

// Dependency Injection
builder.Services.AddScoped<IDataSeeder, CsvDataSeeder>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddControllers()
.AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MovieAPI", Version = "v1" });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "MovieAPI.xml");
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "mymoviedb.csv");
    seeder.SeedMoviesFromCsv(csvPath);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
