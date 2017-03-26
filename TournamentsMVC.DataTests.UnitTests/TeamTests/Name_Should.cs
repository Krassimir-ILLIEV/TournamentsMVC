using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;
using CourseProject.Models;

namespace CourseProject.Models.Tests.GenreTests
{
    [TestFixture]
    public class Name_Should
    {
        [Test]
        public void HaveRequiredAttribute()
        {
            var genre = new Genre();

            var attrs = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttribute()
        {
            var genre = new Genre();

            var attrs = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(MinLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMinLengthAttributeWithCorrectValue()
        {
            var genre = new Genre();

            var attr = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(MinLengthAttribute), false)[0]
                as MinLengthAttribute;

            Assert.AreEqual(3, attr.Length);
        }

        [Test]
        public void HaveMaxLengthAttribute()
        {
            var genre = new Genre();

            var attrs = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveMaxLengthAttributeWithCorrectValue()
        {
            var genre = new Genre();

            var attr = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(MaxLengthAttribute), false)[0]
                as MaxLengthAttribute;

            Assert.AreEqual(25, attr.Length);
        }

        [Test]
        public void HaveIndexAttribute()
        {
            var genre = new Genre();

            var attrs = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(IndexAttribute), false);

            Assert.AreEqual(1, attrs.Length);
        }

        [Test]
        public void HaveIndexWithUniqueTrue()
        {
            var genre = new Genre();

            var attr = genre.GetType()
                .GetProperty("Name")
                .GetCustomAttributes(typeof(IndexAttribute), false)[0]
                as IndexAttribute;

            Assert.AreEqual(true, attr.IsUnique);
        }

        [Test]
        public void HaveGetAndSet()
        {
            var genre = new Genre();

            genre.Name = "some genre";

            Assert.AreEqual("some genre", genre.Name);
        }
    }
}
