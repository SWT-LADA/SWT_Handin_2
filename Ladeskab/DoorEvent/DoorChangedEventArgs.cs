using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.DoorEvent
{
    public class DoorChangedEventArgs : EventArgs
    {
        public bool DoorState { get; set; }
    }
}
