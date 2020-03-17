using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.DisplayFolder;
using Ladeskab.DoorEvent;
using Ladeskab.FileWriterFolder;
using Ladeskab.RFIDEvent;
using Ladeskab.USBChargerFolder;

namespace Ladeskab.Controller
{
    public class StationControl
    {
        private enum LadeskabsState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeskabsState _state;
        private IUSBCharger _charger;
        private IChargeControl _chargeControl;
        private IDoor _door;
        private IDisplay _display;
        private IFileWriter _fileWriter;

        private int _oldID;

        public StationControl(IDoor door, IRFIDReader rfidReader)
        {
            _state = LadeskabsState.Available;
            door.DoorChangedEvent += HandleDoorChangedEvent;
            rfidReader.RFIDChangedEvent += HandleRFIDChangedEvent;
        }
        private void HandleDoorChangedEvent(object s, DoorChangedEventArgs e)
        {
            if (e.DoorState == true) 
            {
                _display.WriteMessage("Connect phone and close the door");
                e.DoorState = false;
            }
            else
            {
                _display.WriteMessage("Read RFID");
            }
        }

        private void HandleRFIDChangedEvent(object s, RFIDChangedEventArgs e)
        {
            switch (_state)
            {
                case LadeskabsState.Available:
                    if (_chargeControl.IsConnected())
                    {
                        _oldID = e.RFID;
                        _charger.StartCharge();
                        _door.LockDoor();
                        _fileWriter.LogDoorLocked(_oldID);
                        _state = LadeskabsState.Locked;
                        
                        _display.WriteMessage("Box is taken and locked with RFID: " +_oldID);
                    }
                    else
                    {
                        _display.WriteMessage("Phone not connected properly, try connecting again");
                    }
                    break;

                case LadeskabsState.DoorOpen:
                    //Gør ingenting
                    break;

                case LadeskabsState.Locked:
                    if (CheckID(e.RFID,_oldID)) //evt. byt om på rækkefølgen af disse
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _fileWriter.LogDoorUnlocked(_oldID);
                        _state = LadeskabsState.Available;

                        _display.WriteMessage("Remove phone");
                    }
                    else
                    {
                        _display.WriteMessage("RFID error");
                    }
                    break;
            }
        }
        private bool CheckID(int OldID, int ID)
        {
            if (OldID == ID) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
