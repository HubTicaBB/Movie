using MovieLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Factory
{
    public class ResponseFactory : AbstractResponseFactory<Response>
    {
        protected override Response CreateResponse(string name) => name switch
        {
            nameof(Ok) => new Ok(),
            nameof(NotFound) => new NotFound(),
            nameof(BadRequest) => new BadRequest(),
            _ => null
        };
    }
}
