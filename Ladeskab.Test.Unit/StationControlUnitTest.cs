using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Controller;
using Ladeskab.DisplayFolder;
using Ladeskab.DoorEvent;
using Ladeskab.FileWriterFolder;
using Ladeskab.RFIDEvent;
using Ladeskab.USBChargerFolder;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    class StationControlUnitTest
    {
        private StationControl _uut;
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        private IUSBCharger _usbCharger;
        private IFileWriter _fileWriter;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _chargeControl = Substitute.For<IChargeControl>();
            _usbCharger = Substitute.For<IUSBCharger>();
            _fileWriter = Substitute.For<IFileWriter>();
            _uut = new StationControl(_door, _rfidReader, _display, _chargeControl, _usbCharger, _fileWriter);
        }

        [Test]
        public void Test_HandleDoorChangedEvent_DoorChangedEventRaised_DoorStateFalse_MethodCallCorrect()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs(){DoorState = false});
            _display.Received().WriteMessage("Read RFID");
        }

        [Test]
        public void Test_HandleDoorChangedEvent_DoorChangedEventRaised_DoorStateTrue_MethodCallCorrect()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs(){DoorState = true});
            _display.Received().WriteMessage("Connect phone and close the door");
        }

        [Test]
        public void Test_HandleRFIDChangedEvent_WhenAvailableAndChargeControlConnected_RFIDChangedEventRaised_MethodCallCorrect()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 10 });
            _door.Received().LockDoor();
            _usbCharger.Received().StartCharge();
            _fileWriter.Received().LogDoorLocked(10);
            _display.Received().WriteMessage("Box is taken and locked with RFID: " + 10);
        }

        [Test]
        public void Test_HandleRFIDChangedEvent_WhenAvailableAndChargeControlNotConnected_RFIDChangedEventRaised_MethodCallCorrect()
        {
            _chargeControl.IsConnected().Returns(false);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 10 });
            _door.DidNotReceive().LockDoor();
            _usbCharger.DidNotReceive().StartCharge();
            _fileWriter.DidNotReceive().LogDoorLocked(10);
            _display.Received().WriteMessage("Phone not connected properly, try connecting again");
        }

        [Test]
        public void Test_HandleRFIDChangedEvent_WhenLockedAndRfidIdentical_RFIDChangedEventRaised_MethodCallCorrect()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 10 });
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 10 });
            _usbCharger.Received().StopCharge();
            _door.Received().UnlockDoor();
            _fileWriter.Received().LogDoorUnlocked(10);
            _display.Received().WriteMessage("Remove phone");


        }

        [Test]
        public void Test_HandleRFIDChangedEvent_WhenLockedAndRfidNotIdentical_RFIDChangedEventRaised_MethodCallCorrect()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 10 });
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs { RFID = 5 });
            _usbCharger.DidNotReceive().StopCharge();
            _door.DidNotReceive().UnlockDoor();
            _fileWriter.DidNotReceive().LogDoorUnlocked(10);
            _display.Received().WriteMessage("RFID error");
        }
    }
}
