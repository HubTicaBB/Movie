using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Responses
{
    public class ResponseObject<T>
    {
        public Response Response { get; set; }
        public T Content { get; set; }
    }
}
