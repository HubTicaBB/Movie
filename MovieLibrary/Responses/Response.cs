using MovieLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
