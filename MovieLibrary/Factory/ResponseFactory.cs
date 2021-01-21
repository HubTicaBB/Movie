using MovieLibrary.Controllers;
using System.Collections.Generic;

namespace MovieLibrary.Factory
{
    public abstract class ResponseFactory<T>
    {
        public T GetResponse(string name, IEnumerable<Movie> movies) => CreateResponse(name, movies);

        protected abstract T CreateResponse(string name, IEnumerable<Movie> movies);
    }
}
