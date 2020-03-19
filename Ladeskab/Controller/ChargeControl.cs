using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.DisplayFolder;
using Ladeskab.USBChargerFolder;

namespace Ladeskab.Controller
{
    public class ChargeControl : IChargeControl
    {
        private IUSBCharger _USBCharger;
        private IDisplay _display;
        public bool _IsConnected { get; set; }
        private string msg;

        public ChargeControl(IUSBCharger usbCharger, IDisplay display )
        {
            _USBCharger = usbCharger;
            _display = display;

            _USBCharger.USBChangedEvent += HandleUSBChangedEvent;
        }

        private void HandleUSBChangedEvent(object s, USBChangedEventArgs e)
        {
            if (e.Current == 0)
            {
                msg = "Der er ingen forbindelse til en telefon, eller ladning er ikke startet " + e.Current;
                _display.WriteMessage(msg);
            }
            else if(e.Current > 0 && e.Current <= 5)
            {
                msg = "Telefonen er fuldt opladet " + e.Current;
                _display.WriteMessage(msg);
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                 msg = "Opladningen foregår normalt " + e.Current;
                _display.WriteMessage(msg);
            }
            else if (e.Current > 500)
            {
                msg = "Fejlmeddelelse: Overload current!" + e.Current;
                _display.WriteMessage(msg);
            }
            else
            {
                throw new System.ArgumentException();
            }
        }

        public bool IsConnected()
        {
            _IsConnected = _USBCharger.Connected;

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
            _USBCharger.StartCharge();
        }

        public void StopCharge()
        {
            _USBCharger.StopCharge();
        }
    }
}
