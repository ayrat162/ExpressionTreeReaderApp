using NUnit.Framework;
using ExpressionTreeReader;

namespace ExpressionTests
{
    public class ToBoolean_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToBoolean_ShouldWorkWithTrue()
        {
            Assert.AreEqual(true, "true".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithFalse()
        {
            Assert.AreEqual(false, "false".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithTrue2()
        {
            Assert.AreEqual(true, "True".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithFalse2()
        {
            Assert.AreEqual(false, "FalsE".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithEmpty()
        {
            Assert.AreEqual(false, "".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithSpace()
        {
            Assert.AreEqual(false, " ".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithOne()
        {
            Assert.AreEqual(true, "1".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithZero()
        {
            Assert.AreEqual(false, "0".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithTen()
        {
            Assert.AreEqual(true, "10".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithMinusOne()
        {
            Assert.AreEqual(false, "-1".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithSpaceAndOne()
        {
            Assert.AreEqual(true, " 1".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithSpacesAndOne()
        {
            Assert.AreEqual(true, " 1 ".ToBoolean());
        }
        
        [Test]
        public void ToBoolean_ShouldWorkWithLetter()
        {
            Assert.AreEqual(false, "a".ToBoolean());
        }
    }
}