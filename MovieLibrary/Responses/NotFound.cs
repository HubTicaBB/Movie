using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Responses
{
    public class NotFound : Response
    {
        public NotFound(IEnumerable<Movie> movies)
        {
            StatusCode = 404;
            Message = "Not Found";
            Movies = movies;
        }
    }
}
