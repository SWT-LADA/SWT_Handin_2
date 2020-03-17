using System;
using NUnit.Framework;
using Ladeskab;
using Ladeskab.Controller;
using Ladeskab.USBChargerFolder;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class ChargeControlUnitTest
    {
        private IChargeControl _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ChargeControl();
        }

        [Test]
        public void IsConnected_IsTrue()
        {
            Assert.That(_uut.IsConnected, Is.True);
        }
    }

    public class USBChargerUnitTest
    {
        private IUSBCharger _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new USBCharger();
        }

        [Test]
        public void IsConnected_IsTrue()
        {
            Assert.That(_uut.Connected, Is.True);
        }

        [Test]
        public void IsConnected_IsFalse()
        {
            _uut.SimulateConnected(false);

            Assert.That(_uut.Connected, Is.False);
        }

        [Test]
        public void CurentValue_IsZero()
        {
            Assert.That(_uut.CurrentValue, Is.Zero);
        }
    }
}
