using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.DoorEvent
{
    public class Door : IDoor
    {
        private bool _oldState = false;
        
        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public bool SetDoorState(bool newState)
        {
            if (newState != _oldState)
            {
                OnDoorChanged(new DoorChangedEventArgs{DoorState = newState});
                _oldState = newState;
            }
            return _oldState;
        }

        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e); //sending an instance of the data to all connected observers 
        }
    }
}
