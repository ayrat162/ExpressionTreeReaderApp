using NUnit.Framework;
using ExpressionTreeReader;

namespace ExpressionTests
{
    public class ToFloat_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToFloat_ShouldWorkWithOne()
        {
            Assert.AreEqual(1.0, "1".ToFloat());
        }
        
        [Test]
        public void ToFloat_ShouldWorkWithZero()
        {
            Assert.AreEqual(0.0, "0".ToFloat());
        }
        
        [Test]
        public void ToFloat_ShouldWorkWithZeroDotZero()
        {
            Assert.AreEqual(0.0, "0.0".ToFloat());
        }
        
        [Test]
        public void ToFloat_ShouldWorkWithDotZero()
        {
            Assert.AreEqual(0.0, ".0".ToFloat());
        }

        [Test]
        public void ToFloat_ShouldWorkWithDotFive()
        {
            Assert.AreEqual(0.5, ".5".ToFloat());
        }

        [Test]
        public void ToFloat_ShouldWorkWithMinusDotFive()
        {
            Assert.AreEqual(-0.5, "-.5".ToFloat());
        }

        [Test]
        public void ToFloat_ShouldWorkWithSpaceAndTwoDotFive()
        {
            Assert.AreEqual(2.5, " 2.5".ToFloat());
        }
        
        [Test]
        public void ToFloat_ShouldWorkWithSpacesAndMinusTwoDotFive()
        {
            Assert.AreEqual(-2.5, " -2.5 ".ToFloat());
        }
    }
}