using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.USBChargerFolder;

namespace Ladeskab.Controller
{
    public class ChargeControl : IChargeControl
    {
        private void HandleUSBChangedEvent(object s, USBChangedEventArgs e)
        {

        }

        public bool IsConnected()
        {
            return true;
        }

        public void StartCharge()
        {

        }

        public void StopCharge()
        {

        }
    }
}
