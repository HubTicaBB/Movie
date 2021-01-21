using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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
        public IEnumerable<string> Toplist(bool asc = false)
        {
            var movies = FetchToplist(client);

            IEnumerable<Movie> orderedMovies = (asc) 
                ? movies.OrderBy(m => m.rated) 
                : movies.OrderByDescending(m => m.rated);

            var titles = orderedMovies.Select(m => m.title).ToList();

            var response = okResponseFactory.GetResponse("Ok");
            return titles;
        }

        [HttpGet]
        [Route("/movie")]
        public Movie GetMovieById(string id) {
            var movies = FetchToplist(client);
            var movie = movies.FirstOrDefault(m => m.id == id);

            return movie;
        }

        [HttpGet]
        [Route("/movies")]
        public IEnumerable<Movie> GetAll()
        {
            var toplistMovies = FetchToplist(client);
            var detailedMovies = FetchDetailed(client);

            var movies = toplistMovies.Concat(detailedMovies);
            var uniqueMovies = movies.GroupBy(m => m.title).Select(m => m.FirstOrDefault()).ToList();

            return uniqueMovies;
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