using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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

        private int status1;
        private int status2;
        private int status3;
        private int status4;

        public ChargeControl(IUSBCharger usbCharger, IDisplay display )
        {
            _USBCharger = usbCharger;
            _display = display;

            _USBCharger.USBChangedEvent += HandleUSBChangedEvent;
        }

        private void HandleUSBChangedEvent(object s, USBChangedEventArgs e)
        {
            if (e.Current == 0 && status1 == 0)
            {
                msg = "** Telefon skal fjernes, telefon ikke tilsluttet eller ladning ikke startet **";
                _display.WriteMessage(msg);

                status1 = 1;
                status2 = 0;
                status3 = 0;
                status4 = 0;
            }
            else if (e.Current > 0 && e.Current <= 5 && status2 == 0)
            {
                msg = "** Telefon tilsluttet - Telefonen er fuldt opladet **";
                _display.WriteMessage(msg);

                status1 = 0;
                status2 = 1;
                status3 = 0;
                status4 = 0;
            }
            else if (e.Current > 5 && e.Current <= 500 && status3 == 0)
            {
                msg = "** Telefon tilsluttet - Opladningen foregår normalt **";
                _display.WriteMessage(msg);

                status1 = 0;
                status2 = 0;
                status3 = 1;
                status4 = 0;
            }
            else if (e.Current > 500 && status4 == 0)
            {
                msg = "** Fejlmeddelelse: Overload current! **";
                _display.WriteMessage(msg);

                status1 = 0;
                status2 = 0;
                status3 = 0;
                status4 = 1;
            }
            else if (e.Current < 0)
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
