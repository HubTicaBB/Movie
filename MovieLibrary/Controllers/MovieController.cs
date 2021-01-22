using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using MovieLibrary.Factory;
using MovieLibrary.Responses;
using MovieLibrary.Helpers;

namespace MovieLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        private static readonly string toplistEndpoint = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json";
        private static readonly string detailedEndpoint = "https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json";

        private readonly HttpClient _httpClient;
        private readonly AbstractResponseFactory<Response> _responseFactory;
        private readonly IApiCaller _apiCaller;

        public MovieController(
            HttpClient httpClient, 
            AbstractResponseFactory<Response> responseFactory, 
            IApiCaller apiCaller)
        {
            _httpClient = httpClient;
            _responseFactory = responseFactory;
            _apiCaller = apiCaller;
        }

        [HttpGet]
        [Route("/toplist")]
        public ResponseObject<IEnumerable<string>> Toplist(bool ascending = false)
        {
            var movies = _apiCaller.FetchMovies(_httpClient, toplistEndpoint);

            var responseType = (movies is null) ? "BadRequest" : "Ok";
            var response = _responseFactory.GetResponse(responseType);

            IEnumerable<Movie> orderedMovies = (ascending) 
                ? movies.OrderBy(m => m.rated) 
                : movies.OrderByDescending(m => m.rated);

            var titles = orderedMovies.Select(m => m.title).ToList();

            return new ResponseObject<IEnumerable<string>>() { Response = response, Content = titles };
        }

        [HttpGet]
        [Route("/movie")]
        public ResponseObject<Movie> GetMovieById(string id) {
            var movies = _apiCaller.FetchMovies(_httpClient, toplistEndpoint);
            var movie = movies.FirstOrDefault(m => m.id == id);

            var responseType = (movie is null) ? "NotFound" : "Ok";
            var response = _responseFactory.GetResponse(responseType);

            return new ResponseObject<Movie>() { Response = response, Content = movie };
        }

        [HttpGet]
        [Route("/movies")]
        public ResponseObject<IEnumerable<Movie>> GetUniqueMovies()
        {
            var toplistMovies = _apiCaller.FetchMovies(_httpClient, toplistEndpoint);
            var detailedMovies = _apiCaller.FetchMovies(_httpClient, detailedEndpoint);

            var responseType = (toplistMovies is null || detailedMovies is null) ? "BadRequest" : "Ok";
            var response = _responseFactory.GetResponse(responseType);

            var movies = toplistMovies.Concat(detailedMovies);
            var uniqueMovies = movies.GroupBy(m => m.title).Select(m => m.FirstOrDefault()).ToList();

            return new ResponseObject<IEnumerable<Movie>>() { Response = response, Content = uniqueMovies };
        }
    }
}