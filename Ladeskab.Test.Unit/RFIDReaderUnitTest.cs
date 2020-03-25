using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.DoorEvent;
using Ladeskab.RFIDEvent;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    class RFIDReaderUnitTest
    {
        private RFIDReader _uut;
        private RFIDChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new RFIDReader();
            _uut.SetRFID(5);

            _uut.RFIDChangedEvent +=
                (o, args) => { _receivedEventArgs = args; };
        }

        [Test]
        public void Test_SetRFID_ID10_EventRaised()
        {
            _uut.SetRFID(10);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void Test_SetRFID_ID10_CorrectRFID()
        {
            _uut.SetRFID(10);
            Assert.That(_receivedEventArgs.RFID, Is.EqualTo(10));
        }
    }
}
