using System;
using NUnit.Framework;
using Ladeskab;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class UnitTest
    {
        private Class1 _uut;

       [SetUp]
        public void SetUp()
        {
            _uut = new Class1();
        }

        [TestCase(1,1)]
        [TestCase(2,2)]
        public void TestMetode_ValueOne_returnOne(int value, int result)
        {
            Assert.That(_uut.testMetode(value), Is.EqualTo(result));
        }
    }
}
