using System.Configuration;
using MakingHttpRequest;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyMovieApi.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<IExternalMovies, ExternalMovies>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<MyMovieContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnectionString")));
}
else
{
    builder.Services.AddDbContext<MyMovieContext>(opt =>
        opt.UseInMemoryDatabase("MyMovies"));
}

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseCors(builder => builder.WithOrigins("http://127.0.0.1:5173").AllowAnyHeader().AllowAnyMethod());
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("https://enchanting-ganache-56f1cf.netlify.app").AllowAnyHeader().AllowAnyMethod());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

