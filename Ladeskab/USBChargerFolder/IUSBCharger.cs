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

        void SimulateConnected(bool connected);
        void SimulateOverload(bool overload);

        double CurrentValue { get; }         //Direct access to the current current value
        bool Connected { get; }         //Require connection status of the phone

    }
}
