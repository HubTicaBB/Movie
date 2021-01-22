using MovieLibrary.Controllers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MovieLibrary.Helpers
{
    public class HttpClientCaller : IHttpClientCaller
    {
        public IEnumerable<Movie> FetchMovies(HttpClient client, string endpoint)
        {
            var result = client.GetAsync(endpoint).Result;
            var movies = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());
            return movies;
        }
    }
}
