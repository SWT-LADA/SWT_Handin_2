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
        public void Test_SimulateConnected_DifferentStates(bool state, bool result)
        {
            _uut.SimulateConnected(state);

            Assert.That(_uut.Connected, Is.EqualTo(result));
        }

        [TestCase(true, 750)]
        public void Test_SimulateOverload_StartCharge_StateTrue(bool state, int currentValueResult)
        {
            _uut.StartCharge();

            _uut.SimulateOverload(state);

            System.Threading.Thread.Sleep(1000);

            Assert.That(_uut.CurrentValue, Is.EqualTo(currentValueResult));
        }

        [TestCase(false, 500)]
        public void Test_SimulateOverload_StartCharge_StateFalse(bool state, int currentValueResult)
        {
            _uut.StartCharge();

            _uut.SimulateOverload(state);

            System.Threading.Thread.Sleep(1000);

            Assert.That(_uut.CurrentValue, Is.LessThan(currentValueResult));
        }

        [Test]
        public void Test_ConnectedTrue_OverloadTrue_CurrentValueGreaterThan750()
        {
            _uut.SimulateOverload(true);
            _uut.SimulateConnected(true);

            _uut.StartCharge();

            System.Threading.Thread.Sleep(1000);

            Assert.That(_uut.CurrentValue, Is.GreaterThanOrEqualTo(750));
        }

        [Test]
        public void Test_CurentValue_IsZero()
        {
            Assert.That(_uut.CurrentValue, Is.Zero);
        }

        [TestCase(0, 500)]
        [TestCase(6250, 2.5)]
        public void Test_StartCharge_CurrentValue_IsEqualTo(int time, double currentValue)
        {
            double lastValue = 0;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(time);

            Assert.That(lastValue, Is.EqualTo(currentValue));
        }

        [TestCase(1000, 500)]
        [TestCase(6000, 50)]
        public void Test_StartCharge_CurrentValue_IsLessThan(int time, int currentValue)
        {
            double lastValue = 0;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(time);

            Assert.That(lastValue, Is.LessThan(currentValue));
        }

        [Test]
        public void Test_StartCharge_IfNotConnected()
        {
            double lastValue = 0;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.SimulateConnected(false);

            _uut.StartCharge();
            
            System.Threading.Thread.Sleep(1000);

            Assert.That(_uut.CurrentValue, Is.EqualTo(0.0));
        }

        [TestCase(0,0.0)]
        [TestCase(1000, 0.0)]
        public void Test_StopCharge_LastValueEqualToZero(int time, double currentValue)
        {
            double lastValue = 0;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(time);

            _uut.StopCharge();

            Assert.That(lastValue, Is.EqualTo(currentValue));
        }

        [TestCase(0, 0.0)]
        [TestCase(1000, 0.0)]
        public void Test_StopCharge_CurrentValueEqualToZero(int time, double currentValue)
        {
            double lastValue = 0;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(time);

            _uut.StopCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(currentValue));
        }

        [TestCase(0)]
        [TestCase(300)]
        [TestCase(1100)]
        [TestCase(1300)]
        public void Test_StartCharge_ReceivedSeveralValues_DifferentValues(int time)
        {
            int numValues = 0;
            int result = (time / 250) + 1;

            _uut.USBChangedEvent += (o, args) => numValues++;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(time);

            Assert.That(numValues, Is.EqualTo(result));
        }
    }
}
