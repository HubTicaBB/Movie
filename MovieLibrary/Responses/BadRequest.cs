using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Responses
{
    public class BadRequest : Response
    {
        public BadRequest()
        {
            StatusCode = 500;
            Message = "Bad Request";
        }
    }
}
