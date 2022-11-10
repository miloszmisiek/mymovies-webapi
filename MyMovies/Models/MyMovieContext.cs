using Microsoft.EntityFrameworkCore;
using MyMovieAPI.Models;

namespace MyMovieApi.Models;

public class MyMovieContext : DbContext
{
    public MyMovieContext(DbContextOptions<MyMovieContext> options)
        : base(options)
    {
    }

    public DbSet<MyMovie> MyMovies { get; set; } = null!;
}