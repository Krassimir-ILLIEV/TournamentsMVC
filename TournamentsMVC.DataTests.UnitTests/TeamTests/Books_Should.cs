using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.GenreTests
{
    [TestFixture]
    public class Books_Should
    {
        [Test]
        public void BeVirtualProperty()
        {
            var genre = new Genre();
            var isVirtual = genre.GetType()
                .GetProperty("Books")
                .GetAccessors()[0].IsVirtual;

            Assert.IsTrue(isVirtual);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var genre = new Genre();
            var books = new List<Book>() { new Mock<Book>().Object, new Mock<Book>().Object };

            genre.Books = books;

            CollectionAssert.AreEqual(books, genre.Books);
        }
    }
}
