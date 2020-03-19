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

        //[Test]
        //public void Unit_test_HandleRFIDChangedEvent()
        //{
        //    _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIDChangedEventArgs {RFID = 10});
        //    //Assert.That();
            
        //}
    }
}
