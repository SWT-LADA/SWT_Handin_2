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

        //[Test]
        //public void Test_HandleUSBChangedEvent_USBChangedEventRaised_MethodCallCorrect1()
        //{
        //    _usbCharger.
        //    _usbCharger.USBChangedEvent += Raise.EventWith(new USBChangedEventArgs {Current = 300});

        //}



    //[TestCase(true, true)]
        //[TestCase(false, false)]
        //public void Test_IsConnected_DifferentStates(bool state, bool result)
        //{
        //    _usbCharger.SimulateConnected(state);

        //    _uut.StartCharge();

        //    Assert.AreEqual(result,_uut.IsConnected());
        //}
    }
}
