using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Responses
{
    public class Ok : Response
    {
        public Ok()
        {
            StatusCode = 200;
            Message = "Ok";
        }
    }
}
