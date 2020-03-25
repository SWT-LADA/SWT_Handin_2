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

        [Test]
        public void Test_StartCharge_IsCalled()
        {
            _uut.StartCharge();

            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void Test_StopCharge_IsCalled()
        {
            _uut.StopCharge();

            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(0, "** Phone must be removed, phone not connected or charging not started **")]
        [TestCase(1, "** Phone connected - The phone is fully charged **")]
        [TestCase(5, "** Phone connected - The phone is fully charged **")]
        [TestCase(6, "** Phone connected - Charging is normal **")]
        [TestCase(500, "** Phone connected - Charging is normal **")]
        [TestCase(501, "** Error: Overload current! **")]

        public void Test_HandleUSBChangedEvent_USBChangedEventRaised_DifferentCurrentValues(int currentValue, string msg)
        {
            _usbCharger.USBChangedEvent += Raise.EventWith(new USBChangedEventArgs { Current = currentValue });
            _display.Received().WriteMessage(msg);
        }

        [Test]
        public void Test_HandleUSBChangedEvent_USBChangedEventRaised_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => _usbCharger.USBChangedEvent += Raise.EventWith(new USBChangedEventArgs { Current = -1 }));
        }

        [TestCase(false, false)]
        [TestCase(true, true)]

        public void Test_IsConnectedProperty_DifferentStates(bool input, bool result)
        {
            _uut._IsConnected = input;

            Assert.AreEqual(result,_uut._IsConnected);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public void Test_IsConnected_DifferentStates(bool state, bool result)
        {
            _usbCharger.Connected.Returns(state);

            _uut.IsConnected();

            Assert.AreEqual(result, _uut.IsConnected());
        }
    }
}
