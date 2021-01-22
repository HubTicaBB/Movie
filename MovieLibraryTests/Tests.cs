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
        public void Toplist_ArgIsFalse_ReturnsMoviesDescending()
        {

        }

        [TestMethod]
        public void Toplist_ArgIsTrue_ReturnsMoviesAscending()
        {

        }
    }
}
