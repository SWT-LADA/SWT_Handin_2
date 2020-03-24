using System;
using System.IO;
using NUnit.Framework;
using Ladeskab;
using Ladeskab.DisplayFolder;
using NSubstitute;
using NUnit.Framework.Constraints;

//using NSubstitute;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class DisplayUnitTest
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [TestCase("Test")]
        public void Test(string input)
        {
            //mangler at kunne teste om det den metode, der er void, men udskriver på konsol, udskriver, er det samme som inputtet
            _uut.WriteMessage(input); 
            //Assert.That(_uut.WriteMessage(input),Contains.Item())};
           //Assert.That(_uut.WriteMessage(input),Is.EqualTo("Test"));
           //// _uut.Received().WriteMessage("Test");
        }
    }
}
