using System;
using System.IO;
using NUnit.Framework;
using Ladeskab;
using Ladeskab.DisplayFolder;
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

        [Test]
        public void Test()
        {
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);
            TextReader reader = new StreamReader(stream);

        }
    }
}
