using MovieLibrary.Controllers;
using MovieLibrary.Helpers;
using System.Collections.Generic;
using System.Net.Http;

namespace MovieLibraryTests.Mock
{
    public class MockApiCaller : IApiCaller
    {
        public IEnumerable<Movie> FetchMovies(HttpClient client, string endpoint)
        {
            return new List<Movie> 
            { 
                new Movie() { id = "1", rated = "1", title = "1" },
                new Movie() { id = "2", rated = "2", title = "2" }
            };
        }
    }
}
