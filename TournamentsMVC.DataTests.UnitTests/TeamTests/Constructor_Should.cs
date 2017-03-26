using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.GenreTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void InitializeBooks()
        {
            var genre = new Genre();

            Assert.IsNotNull(genre.Books);
        }
    }
}
