using MovieLibrary.Controllers;
using MovieLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryTests.Mock
{
    public class MockHttpClientCaller : IHttpClientCaller
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
