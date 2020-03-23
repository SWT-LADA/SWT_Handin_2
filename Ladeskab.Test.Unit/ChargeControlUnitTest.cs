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

        [TestCase(0, "** Telefon skal fjernes, telefon ikke tilsluttet eller ladning ikke startet **")]
        [TestCase(1, "** Telefon tilsluttet - Telefonen er fuldt opladet **")]
        [TestCase(5, "** Telefon tilsluttet - Telefonen er fuldt opladet **")]
        [TestCase(6, "** Telefon tilsluttet - Opladningen foregår normalt **")]
        [TestCase(500, "** Telefon tilsluttet - Opladningen foregår normalt **")]
        [TestCase(501, "** Fejlmeddelelse: Overload current! **")]

        public void Test_HandleUSBChangedEvent_USBChangedEventRaised_DifferentCurrentValues(int currentValue, string msg)
        {
            _usbCharger.USBChangedEvent += Raise.EventWith(new USBChangedEventArgs { Current = currentValue });
            _display.Received().WriteMessage(msg);
        }



        //[TestCase(true, true)]
        //public void Test_IsConnected_DifferentStates(bool state, bool result)
        //{
        //    _usbCharger.SimulateConnected(true);

        //    Assert.AreEqual(result, _uut.IsConnected());
        //}
    }
}
