using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.USBChargerFolder
{
    public interface IUSBCharger
    {
        event EventHandler<USBChangedEventArgs> USBChangedEvent;
        void StartCharge();
        void StopCharge();

        //Direct access to the current current value
        double CurrentValue { get; }

        //Require connection status of the phone
        bool Connected { get; }
    }
}
