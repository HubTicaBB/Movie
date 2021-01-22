using MovieLibrary.Controllers;
using System.Collections.Generic;
using System.Net.Http;

namespace MovieLibrary.Helpers
{
    public interface IApiCaller
    {
        public IEnumerable<Movie> FetchMovies(HttpClient client, string endpoint);
    }
}
