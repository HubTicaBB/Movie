using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MovieLibraryTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Toplist_ReturnsListOfStrings()
        {
            var controller = new MovieController();

            var actual = controller.Toplist().ToList();

            Assert.IsInstanceOfType( actual, typeof(List<string>));
        }

        [TestMethod]
        public void Toplist_ReturnsCount100()
        {
            var controller = new MovieController();

            var actual = controller.Toplist().ToList().Count;

            Assert.AreEqual(100, actual);
        }

        [TestMethod]
        public void Toplist_ReturnsDescending()
        {
            var controller = new MovieController();

            var actual = controller.Toplist();
            var movies = controller.Toplist().ToList();
            var ordered = movies.Toplist(x => x.rated).ToList();
            var expected = ordered.Select(x => x.title).ToList();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Toplist_ReturnsAscending()
        {
            var controller = new MovieController(new HttpClient());

            var actual = controller.Toplist(true);
            var movies = controller.Toplist(new HttpClient()).ToList();
            var ordered = movies.OrderBy(x => x.rated).ToList();
            var expected = ordered.Select(x => x.title).ToList();

            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
