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
        private IUSBCharger _usbCharger;
        public bool _IsConnected { get; set; }

        public ChargeControl()
        {
            _usbCharger = new USBCharger();
        }

        private void HandleUSBChangedEvent(object s, USBChangedEventArgs e)
        {

        }

        public bool IsConnected()
        {
            _IsConnected = _usbCharger.Connected;

            if (_IsConnected == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}
