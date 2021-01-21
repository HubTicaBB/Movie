using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using MovieLibrary.Factory;
using MovieLibrary.Responses;

namespace MovieLibrary.Controllers
{
    public class Movie
    {
        public string id { get; set; }
        public string title { get; set; }
        public string rated { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        static HttpClient client = new HttpClient();
        ResponseFactory<Response> okResponseFactory = new OkResponseFactory();
        ResponseFactory<Response> errorResponseFactory = new ErrorResponseFactory();

        [HttpGet]
        [Route("/toplist")]
        public ResponseObject<IEnumerable<string>> Toplist(bool asc = false)
        {
            var movies = FetchToplist(client);

            var response = (movies is null)
                ? errorResponseFactory.GetResponse("BadRequest")
                : okResponseFactory.GetResponse("Ok");

            IEnumerable<Movie> orderedMovies = (asc) 
                ? movies.OrderBy(m => m.rated) 
                : movies.OrderByDescending(m => m.rated);

            var titles = orderedMovies.Select(m => m.title).ToList();

            return new ResponseObject<IEnumerable<string>>() { Response = response, Content = titles };
        }

        [HttpGet]
        [Route("/movie")]
        public ResponseObject<Movie> GetMovieById(string id) {
            var movies = FetchToplist(client);
            var movie = movies.FirstOrDefault(m => m.id == id);

            var response = (movie is null)
                ? errorResponseFactory.GetResponse("NotFound")
                : okResponseFactory.GetResponse("Ok");

            return new ResponseObject<Movie>() { Response = response, Content = movie };
        }

        [HttpGet]
        [Route("/movies")]
        public ResponseObject<IEnumerable<Movie>> GetAll()
        {
            var toplistMovies = FetchToplist(client);
            var detailedMovies = FetchDetailed(client);

            var response = (toplistMovies is null || detailedMovies is null)
                ? errorResponseFactory.GetResponse("BadRequest")
                : okResponseFactory.GetResponse("Ok");

            var movies = toplistMovies.Concat(detailedMovies);
            var uniqueMovies = movies.GroupBy(m => m.title).Select(m => m.FirstOrDefault()).ToList();

            return new ResponseObject<IEnumerable<Movie>>() { Response = response, Content = uniqueMovies };
        }

        private static IEnumerable<Movie> FetchToplist(HttpClient client)
        {
            var result = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var movies = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());
            return movies;
        }

        private static IEnumerable<Movie> FetchDetailed(HttpClient client)
        {
            var result = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;
            var movies = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());
            return movies;
        }
    }
}