using System;
using NUnit.Framework;
using Ladeskab;
using Ladeskab.Controller;
using Ladeskab.DisplayFolder;
using Ladeskab.DoorEvent;
using Ladeskab.USBChargerFolder;
using NSubstitute;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class ChargeControlUnitTest
    {
        private IChargeControl _uut;
        private IDisplay _display;
        private IUSBCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUSBCharger>();
            _uut = new ChargeControl(_usbCharger, _display);
        }

        //[Test]
        //public void IsConnected_IsTrue()
        //{
        //    Assert.That(_uut.IsConnected, Is.True);
        //}
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
