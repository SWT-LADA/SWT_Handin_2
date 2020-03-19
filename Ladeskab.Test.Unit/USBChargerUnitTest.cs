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
        [TestCase(false, 500)]
        public void Unit_test_SimulateOverload_DifferentStates(bool state, int result)
        {
            _uut.SimulateOverload(state);

            _uut.StartCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(result));
        }

        [Test]
        public void Unit_test_StartCharge_ZeroSeconds()
        {
            double lastValue = 1000;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(0);

            Assert.That(lastValue, Is.EqualTo(500));
        }

        [Test]
        public void Unit_test_StartCharge_OneSecond()
        {
            double lastValue = 1000;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(1000);

            Assert.That(lastValue, Is.LessThan(500));
        }

        [Test]
        public void Unit_test_StartCharge_MoreThanSixtySeconds()
        {
            double lastValue = 1000;

            _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();

            System.Threading.Thread.Sleep(63000);

            Assert.That(lastValue, Is.EqualTo(2.5));
        }


        //*********************************
        //[Test]
        //public void ctor_IsConnected()
        //{
        //    Assert.That(_uut.Connected, Is.True);
        //}

        //[Test]
        //public void ctor_CurentValueIsZero()
        //{
        //    Assert.That(_uut.CurrentValue, Is.Zero);
        //}

        //[Test]
        //public void SimulateDisconnected_ReturnsDisconnected()
        //{
        //    _uut.SimulateConnected(false);
        //    Assert.That(_uut.Connected, Is.False);
        //}

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

        //[Test]
        //public void StartedNoEventReceiver_WaitSomeTime_PropertyChangedValue()
        //{
        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    Assert.That(_uut.CurrentValue, Is.LessThan(500.0));
        //}

        //[Test]
        //public void Started_WaitSomeTime_PropertyMatchesReceivedValue()
        //{
        //    double lastValue = 1000;
        //    _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(1100);

        //    Assert.That(lastValue, Is.EqualTo(_uut.CurrentValue));
        //}


        //[Test]
        //public void Started_SimulateOverload_ReceivesHighValue()
        //{
        //    ManualResetEvent pause = new ManualResetEvent(false);
        //    double lastValue = 0;

        //    _uut.USBChangedEvent += (o, args) =>
        //    {
        //        lastValue = args.Current;
        //        pause.Set();
        //    };

        //    // Start
        //    _uut.StartCharge();

        //    // Next value should be high
        //    _uut.SimulateOverload(true);

        //    // Reset event
        //    pause.Reset();

        //    // Wait for next tick, should send overloaded value
        //    pause.WaitOne(300);

        //    Assert.That(lastValue, Is.GreaterThan(500.0));
        //}

        //[Test]
        //public void Started_SimulateDisconnected_ReceivesZero()
        //{
        //    ManualResetEvent pause = new ManualResetEvent(false);
        //    double lastValue = 1000;

        //    _uut.USBChangedEvent += (o, args) =>
        //    {
        //        lastValue = args.Current;
        //        pause.Set();
        //    };


        //    // Start
        //    _uut.StartCharge();

        //    // Next value should be zero
        //    _uut.SimulateConnected(false);

        //    // Reset event
        //    pause.Reset();

        //    // Wait for next tick, should send disconnected value
        //    pause.WaitOne(300);

        //    Assert.That(lastValue, Is.Zero);
        //}

        //[Test]
        //public void SimulateOverload_Start_ReceivesHighValueImmediately()
        //{
        //    double lastValue = 0;

        //    _uut.USBChangedEvent += (o, args) =>
        //    {
        //        lastValue = args.Current;
        //    };

        //    // First value should be high
        //    _uut.SimulateOverload(true);

        //    // Start
        //    _uut.StartCharge();

        //    // Should not wait for first tick, should send overload immediately

        //    Assert.That(lastValue, Is.GreaterThan(500.0));
        //}

        //[Test]
        //public void SimulateDisconnected_Start_ReceivesZeroValueImmediately()
        //{
        //    double lastValue = 1000;

        //    _uut.USBChangedEvent += (o, args) =>
        //    {
        //        lastValue = args.Current;
        //    };

        //    // First value should be high
        //    _uut.SimulateConnected(false);

        //    // Start
        //    _uut.StartCharge();

        //    // Should not wait for first tick, should send zero immediately

        //    Assert.That(lastValue, Is.Zero);
        //}

        //[Test]
        //public void StopCharge_IsCharging_ReceivesZeroValue()
        //{
        //    double lastValue = 1000;
        //    _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    _uut.StopCharge();

        //    Assert.That(lastValue, Is.EqualTo(0.0));
        //}

        //[Test]
        //public void StopCharge_IsCharging_PropertyIsZero()
        //{
        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    _uut.StopCharge();

        //    Assert.That(_uut.CurrentValue, Is.EqualTo(0.0));
        //}

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

        //*******************************************************


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



        //[Test]
        //public void StopCharge_IsCharging_ReceivesZeroValue()
        //{
        //    double lastValue = 1000;
        //    _uut.USBChangedEvent += (o, args) => lastValue = args.Current;

        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(500);

        //    _uut.StopCharge();

        //    Assert.That(lastValue, Is.EqualTo(0.0));
        //}

        //[Test]
        //public void StopCharge_IsCharging_PropertyIsZero()
        //{
        //    _uut.StartCharge();

        //    System.Threading.Thread.Sleep(300);

        //    _uut.StopCharge();

        //    Assert.That(_uut.CurrentValue, Is.EqualTo(0.0));
        //}

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
