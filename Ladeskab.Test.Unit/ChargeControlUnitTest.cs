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

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Unit_test_SIsConnected_DifferentStates(bool state, bool result)
        {
            //_usbCharger.SimulateConnected(state);

            //Assert.That(_uut.IsConnected, Is.EqualTo(result));
        }
    }
}
