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
        protected override Response CreateResponse(string name) => name switch
        {
            nameof(NotFound) => new NotFound(),
            nameof(BadRequest) => new BadRequest(),
            _ => null
        };
    }
}
