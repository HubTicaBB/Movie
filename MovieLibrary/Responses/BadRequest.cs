using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Responses
{
    public class BadRequest : Response
    {
        public BadRequest(IEnumerable<Movie> movies)
        {
            StatusCode = 500;
            Message = "Bad Request";
            Movies = movies;
        }
    }
}
