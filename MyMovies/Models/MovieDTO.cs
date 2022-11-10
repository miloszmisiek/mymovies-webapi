namespace MyMovieAPI.Models;

public class MyMovie
{
	public long Id { get; set; }
	public string? Title { get; set; }
	public string? Director { get; set; }
	public Int32 Year { get; set; }
	public float Rate { get; set; }
}

