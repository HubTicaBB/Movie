using MovieLibrary.Controllers;
using MovieLibrary.Responses;
using System.Collections.Generic;

namespace MovieLibrary.Factory
{
    public class OkResponseFactory : ResponseFactory<Response>
    {
        protected override Response CreateResponse(string name) => name switch
        {
            nameof(Ok) => new Ok(),
            _ => null
        };
    }
}
