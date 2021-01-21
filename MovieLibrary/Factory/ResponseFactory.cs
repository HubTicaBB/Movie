using MovieLibrary.Controllers;
using System.Collections.Generic;

namespace MovieLibrary.Factory
{
    public abstract class ResponseFactory<T>
    {
        public T GetResponse(string name) => CreateResponse(name);

        protected abstract T CreateResponse(string name);
    }
}
