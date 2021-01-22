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
        private readonly HttpClient client;
        private readonly AbstractResponseFactory<Response> factory;

        public MovieController(HttpClient httpClient, AbstractResponseFactory<Response> responseFactory)
        {
            client = httpClient;
            factory = responseFactory;
        }

        [HttpGet]
        [Route("/toplist")]
        public ResponseObject<IEnumerable<string>> Toplist(bool asc = false)
        {
            var movies = FetchToplist(client);

            var responseType = (movies is null) ? "BadRequest" : "Ok";
            var response = factory.GetResponse(responseType);

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

            var responseType = (movie is null) ? "NotFound" : "Ok";
            var response = factory.GetResponse(responseType);

            return new ResponseObject<Movie>() { Response = response, Content = movie };
        }

        [HttpGet]
        [Route("/movies")]
        public ResponseObject<IEnumerable<Movie>> GetAll()
        {
            var toplistMovies = FetchToplist(client);
            var detailedMovies = FetchDetailed(client);

            var responseType = (toplistMovies is null || detailedMovies is null) ? "BadRequest" : "Ok";
            var response = factory.GetResponse(responseType);

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