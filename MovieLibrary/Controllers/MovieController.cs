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
        static string toplistEndpoint = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json";
        static string detailedEndpoint = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json";

        ResponseFactory<Response> okResponseFactory = new OkResponseFactory();
        ResponseFactory<Response> errorResponseFactory = new ErrorResponseFactory();

        [HttpGet]
        [Route("/toplist")]
        public ResponseObject<IEnumerable<string>> Toplist(bool asc = false)
        {
            var movies = FetchMovies(client, toplistEndpoint);

            var response = GetResponse(movies);

            IEnumerable<Movie> orderedMovies = (asc) 
                ? movies.OrderBy(m => m.rated) 
                : movies.OrderByDescending(m => m.rated);

            var titles = orderedMovies.Select(m => m.title).ToList();

            return new ResponseObject<IEnumerable<string>>() { Response = response, Content = titles };
        }

        [HttpGet]
        [Route("/movie")]
        public ResponseObject<Movie> GetMovieById(string id) {
            var movies = FetchMovies(client, toplistEndpoint);
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
            var toplistMovies = FetchMovies(client, toplistEndpoint);
            var detailedMovies = FetchMovies(client, detailedEndpoint);

            var response = (toplistMovies is null || detailedMovies is null)
                ? errorResponseFactory.GetResponse("BadRequest")
                : okResponseFactory.GetResponse("Ok");

            var movies = toplistMovies.Concat(detailedMovies);
            var uniqueMovies = movies.GroupBy(m => m.title).Select(m => m.FirstOrDefault()).ToList();

            return new ResponseObject<IEnumerable<Movie>>() { Response = response, Content = uniqueMovies };
        }

        private static IEnumerable<Movie> FetchMovies(HttpClient client, string endpoint)
        {
            var result = client.GetAsync(endpoint).Result;
            var movies = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());
            return movies;
        }

        private Response GetResponse(IEnumerable<Movie> movies)
        {
            return (movies is null)
                ? errorResponseFactory.GetResponse("BadRequest")
                : okResponseFactory.GetResponse("Ok");
        }
    }
}