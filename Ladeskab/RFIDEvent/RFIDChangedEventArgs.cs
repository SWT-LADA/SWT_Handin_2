using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.RFIDEvent
{
    public class RFIDChangedEventArgs : EventArgs
    {
        public int RFID { get; set; }
    }
}
