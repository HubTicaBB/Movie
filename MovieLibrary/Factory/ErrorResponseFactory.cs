using MovieLibrary.Controllers;
using MovieLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Factory
{
    public class ErrorResponseFactory : ResponseFactory<Response>
    {
        protected override Response CreateResponse(string name, IEnumerable<Movie> movies) => name switch
        {
            nameof(NotFound) => new NotFound(movies),
            nameof(BadRequest) => new BadRequest(movies),
            _ => null
        };
    }
}
