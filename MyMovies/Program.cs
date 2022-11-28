using System.Configuration;
using MakingHttpRequest;
using Microsoft.EntityFrameworkCore;
using MyMovieApi.Models;


var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("Movies") ?? "Data Source=Movies.db";
//var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ;

builder.Services.AddControllers();
builder.Services.AddHttpClient<IExternalMovies, ExternalMovies>();

builder.Services.AddDbContext<MyMovieContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTION")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

