using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieLibrary.Controllers;
using MovieLibrary.Factory;
using MovieLibrary.Helpers;
using MovieLibraryTests.Mock;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MovieLibraryTests
{
    [TestClass]
    public class Tests
    {
        private IHttpClientCaller mockClientCaller;
        private HttpClient httpClient;
        private ResponseFactory factory;

        [TestInitialize]
        public void TestInitialize()
        {
            mockClientCaller = new MockHttpClientCaller();
            httpClient = new HttpClient();
            factory = new ResponseFactory();            
        }

        [TestMethod]
        public void Toplist_NoArgument_ReturnsMoviesDescending()
        {
            var controller = new MovieController(httpClient, factory, mockClientCaller);

            var result = controller.Toplist().Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last + 1 );
        }

        [TestMethod]
        public void Toplist_AscendingIsFalse_ReturnsMoviesDescending()
        {
            var controller = new MovieController(httpClient, factory, mockClientCaller);

            var result = controller.Toplist(false).Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last + 1);
        }

        [TestMethod]
        public void Toplist_AscendingIsIsTrue_ReturnsMoviesAscending()
        {
            var controller = new MovieController(httpClient, factory, mockClientCaller);

            var result = controller.Toplist(true).Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last - 1);
        }

        [TestMethod]
        public void GetMovieById_ExistingId_ReturnsOk()
        {
            var controller = new MovieController(httpClient, factory, mockClientCaller);

            var actual = controller.GetMovieById("1").Response.StatusCode;

            Assert.AreEqual(200, actual);
        }
        
        [TestMethod]
        public void GetMovieById_NonExistingId_ReturnsNotFound()
        {
            var controller = new MovieController(httpClient, factory, mockClientCaller);

            var actual = controller.GetMovieById("0").Response.StatusCode;

            Assert.AreEqual(404, actual);
        }

        [TestMethod]
        public void GetAllUnique_WhenCalled_ReturnsNoDuplicates()
        {
            var mock = new Mock<IHttpClientCaller>();
            mock.Setup(m => m.FetchMovies(httpClient, "endpoint")).Returns(() => 
                new List<Movie> 
                {
                    new Movie() { id = "1", rated = "1", title = "1" },
                    new Movie() { id = "2", rated = "2", title = "2" },
                    new Movie() { id = "3", rated = "3", title = "2" }
                });
            var controller = new MovieController(httpClient, factory, mock.Object);

            var actual = controller.GetAllUnique();
        }
    }
}
