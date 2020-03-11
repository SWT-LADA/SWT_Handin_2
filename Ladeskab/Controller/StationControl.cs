using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.DoorEvent;
using Ladeskab.RFIDEvent;

namespace Ladeskab.Controller
{
    public class StationControl
    {
        private void HandleDoorChangedEvent(object s, DoorChangedEventArgs e)
        {

        }

        private void HandleRFIDChangedEvent(object s, RFIDChangedEventArgs e)
        {

        }

        private bool CheckID(int OldID, int ID)
        {
            return true;
        }
    }
}
