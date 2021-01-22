using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieLibrary.Helpers
{
    public interface IApiCaller
    {
        public IEnumerable<Movie> FetchMovies(HttpClient client, string endpoint);
    }
}
