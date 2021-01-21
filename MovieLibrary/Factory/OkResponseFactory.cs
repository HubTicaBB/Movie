using MovieLibrary.Controllers;
using MovieLibrary.Responses;
using System.Collections.Generic;

namespace MovieLibrary.Factory
{
    public class OkResponseFactory : ResponseFactory<Response>
    {
        protected override Response CreateResponse(string name, IEnumerable<Movie> movies) => name switch
        {
            nameof(Ok) => new Ok(movies),
            _ => null
        };
    }
}
