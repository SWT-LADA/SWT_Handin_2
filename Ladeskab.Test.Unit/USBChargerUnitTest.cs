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

        [Test]
        public void Started_WaitSomeTime_ReceivedSeveralValues()
        {
            int numValues = 0;
            _uut.USBChangedEvent += (o, args) => numValues++;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(1100);

            Assert.That(numValues, Is.GreaterThan(4));
        }

        [Test]
        public void Started_WaitSomeTime_ReceivedChangedValue()
        {
            double lastValue = 1000;
            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(300);

            Assert.That(lastValue, Is.LessThan(500.0));
        }
    }
}
