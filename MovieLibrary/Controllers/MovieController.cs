using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet]
        [Route("/toplist")]
        public IEnumerable<string> Toplist(bool asc = false)
        {
            var movies = FetchMovies(client);

            IEnumerable<Movie> orderedMovies = (asc) 
                ? movies.OrderBy(m => m.rated) 
                : movies.OrderByDescending(m => m.rated);

            var titles = orderedMovies.Select(m => m.title).ToList();
            return titles;
        }

        [HttpGet]
        [Route("/movie")]
        public Movie GetMovieById(string id) {
            var movies = FetchMovies(client);
            var movie = movies.FirstOrDefault(m => m.id == id);

            return movie;
        }

        private static IEnumerable<Movie> FetchMovies(HttpClient client)
        {
            var result = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var movies = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());
            return movies;
        }
    }
}