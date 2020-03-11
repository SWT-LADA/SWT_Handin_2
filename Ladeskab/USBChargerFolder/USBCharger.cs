using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.USBChargerFolder
{
    public class USBCharger : IUSBCharger
    {
        public event EventHandler<USBChangedEventArgs> USBChangedEvent;
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        public void SetUSB(bool USB)
        {

        }

        protected virtual void OnUSBChanged(USBChangedEventArgs e)
        {
            USBChangedEvent?.Invoke(this, e);
        }
    }
}
