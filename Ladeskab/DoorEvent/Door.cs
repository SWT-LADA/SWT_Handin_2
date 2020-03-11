using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.DoorEvent
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public bool SetDoorState()
        {

        }

        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            
        }
    }
}
