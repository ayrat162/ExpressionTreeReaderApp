using NUnit.Framework;
using ExpressionTreeReader;

namespace ExpressionTests
{
    public class ToInt_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToInt_ShouldWorkWithOne()
        {
            Assert.AreEqual(1, "1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithZero()
        {
            Assert.AreEqual(0, "0".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithMinusOne()
        {
            Assert.AreEqual(-1, "-1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithPlusOne()
        {
            Assert.AreEqual(1, "+1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithMillion()
        {
            Assert.AreEqual(1000000, "1000000".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithFloat()
        {
            Assert.AreEqual(0, "0.1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithLetter()
        {
            Assert.AreEqual(0, "a".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithEmpty()
        {
            Assert.AreEqual(0, "".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithSpace()
        {
            Assert.AreEqual(0, " ".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithSpaceAndOne()
        {
            Assert.AreEqual(1, " 1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithSpacesAndOne()
        {
            Assert.AreEqual(1, " 1 ".ToInt());
        }
        [Test]
        public void ToInt_ShouldWorkWithTrue()
        {
            Assert.AreEqual(1, "true".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithFalse()
        {
            Assert.AreEqual(0, "false".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithZeroDotOne()
        {
            Assert.AreEqual(0, "0.1".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithOneDotFive()
        {
            Assert.AreEqual(1, "1.5".ToInt());
        }
        
        [Test]
        public void ToInt_ShouldWorkWithMinusOneDotFive()
        {
            Assert.AreEqual(-1, "-1.5".ToInt());
        }
    }
}