using System;
namespace MakingHttpRequest
{
    public interface IExternalMovies {
        Task<string> Get();
    }

    public class ExternalMovies : IExternalMovies
    {
        private HttpClient _httpClient;

        public ExternalMovies(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get()
        {
            string APIURL = "https://filmy.programdemo.pl/MyMovies";
            var response = await _httpClient.GetAsync(APIURL);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

