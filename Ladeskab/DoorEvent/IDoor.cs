using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.DoorEvent
{
    public interface IDoor
    {
        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        bool SetDoorState(bool NewState);

        void LockDoor();

        void UnlockDoor();
    }
}
