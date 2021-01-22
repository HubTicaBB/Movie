using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Factory;
using MovieLibrary.Helpers;
using MovieLibraryTests.Mock;
using System.Linq;
using System.Net.Http;

namespace MovieLibraryTests
{
    [TestClass]
    public class Tests
    {
        private IApiCaller _mockApiCaller;
        private HttpClient _httpClient;
        private ResponseFactory _responseFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockApiCaller = new MockApiCaller();
            _httpClient = new HttpClient();
            _responseFactory = new ResponseFactory();            
        }

        [TestMethod]
        public void Toplist_NoArgument_ReturnsMoviesDescending()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);
            var result = controller.Toplist().Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last + 1 );
        }

        [TestMethod]
        public void Toplist_AscendingIsFalse_ReturnsMoviesDescending()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);

            var result = controller.Toplist(false).Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last + 1);
        }

        [TestMethod]
        public void Toplist_AscendingIsIsTrue_ReturnsMoviesAscending()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);

            var result = controller.Toplist(true).Content;
            var first = int.Parse(result.First());
            var last = int.Parse(result.Last());

            Assert.AreEqual(first, last - 1);
        }

        [TestMethod]
        public void GetMovieById_ExistingId_ReturnsOk()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);

            var actual = controller.GetMovieById("1").Response.StatusCode;

            Assert.AreEqual(200, actual);
        }

        [TestMethod]
        public void GetMovieById_NonExistingId_ReturnsNotFound()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);

            var actual = controller.GetMovieById("0").Response.StatusCode;

            Assert.AreEqual(404, actual);
        }

        [TestMethod]
        public void GetUniqueMovies_WhenCalled_ReturnsNoDuplicates()
        {
            var controller = new MovieController(_httpClient, _responseFactory, _mockApiCaller);

            var uniqueMovies = controller.GetUniqueMovies().Content;

            Assert.IsTrue(uniqueMovies.Distinct().Count() == uniqueMovies.Count());
        }
    }
}
