using System;
using System.Threading;
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
    public class USBChargerUnitTest
    {
        private IUSBCharger _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new USBCharger();
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Unit_test_SimulateConnected_DifferentStates(bool state, bool result)
        {
            _uut.SimulateConnected(state);

            Assert.That(_uut.Connected, Is.EqualTo(result));
        }

        [TestCase(true, 750)]
        public void Unit_test_SimulateOverload_DifferentStates(bool state, int result)
        {
            _uut.SimulateOverload(state);

            _uut.StartCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(result));
        }

        //[Test]
        //public void Started_WaitSomeTime_ReceivedSeveralValues()
        //{
        //    int numValues = 0;
        //    _uut.USBChangedEvent += (o, args) => numValues++;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(1100);

        //    Assert.That(numValues, Is.GreaterThan(4));
        //}

        //[Test]
        //public void Started_WaitSomeTime_ReceivedChangedValue()
        //{
        //    double lastValue = 1000;
        //    _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    Assert.That(lastValue, Is.LessThan(500.0));
        //}

        [Test]
        public void StopCharge_IsCharging_ReceivesZeroValue()
        {
            double lastValue = 1000;
            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(500);

            _uut.StopCharge();

            Assert.That(lastValue, Is.EqualTo(0.0));
        }

        [Test]
        public void StopCharge_IsCharging_PropertyIsZero()
        {
            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            _uut.StopCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(0.0));
        }

        //[Test]
        //public void StopCharge_IsCharging_ReceivesNoMoreValues()
        //{
        //    double lastValue = 1000;
        //    _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    _uut.StopCharge();
        //    lastValue = 1000;

        //    // Wait for a tick
        //    System.Threading.Thread.Sleep(300);

        //    // No new value received
        //    Assert.That(lastValue, Is.EqualTo(1000.0));
        //}

    }
}
