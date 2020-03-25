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

        [TestCase("Testing if this works")]
        public void Test_WriteMessage_output(string input)
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _uut.WriteMessage(input);
            Assert.That(stringWriter.ToString().Contains(input));
        }
    }
}
