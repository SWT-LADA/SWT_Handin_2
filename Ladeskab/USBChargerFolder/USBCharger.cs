﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Ladeskab.USBChargerFolder
{
    public class USBCharger : IUSBCharger
    {
        // Constants
        private const double MaxCurrent = 500.0; // mA
        private const double FullyChargedCurrent = 2.5; // mA 
        private const double OverloadCurrent = 750; // mA
        private const double ChargeTimeMinutes = 0.1; // Det tager 6 sek at oplade telefonen ** HER DER SKAL ÆNDRES LADETID ***
        private const int CurrentTickInterval = 250; // Sætter timer interval til 0.25 sekunder (250 ms)

        public event EventHandler<USBChangedEventArgs> USBChangedEvent;

        public double CurrentValue { get; private set; }

        public bool Connected { get; private set; }

        private bool _overload;
        private bool _charging;
        private System.Timers.Timer _timer;
        private int _ticksSinceStart;

        public USBCharger()
        {
            CurrentValue = 0.0;
            Connected = true;
            _overload = false;

            _timer = new System.Timers.Timer(); 
            _timer.Enabled = false; // Når den er lig false raiser den ikke elasped event. Sættes automatisk til true når man kalder _timer.Start()
            _timer.Interval = CurrentTickInterval; // Timer "ticker" for hver 250 ms
            _timer.Elapsed += TimerOnElapsed; // Kalder TimerOnElapsed for hvert tick
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_charging)
            {
                _ticksSinceStart++;
                if (Connected && !_overload)
                {
                    double newValue = MaxCurrent -
                                      _ticksSinceStart * (MaxCurrent - FullyChargedCurrent) 
                                      / (ChargeTimeMinutes * 60 * 1000 / CurrentTickInterval);
                    CurrentValue = Math.Max(newValue, FullyChargedCurrent);
                }
                else if (Connected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!Connected)
                {
                    CurrentValue = 0.0;
                }

                OnUSBChanged();
            }
        }

        public void SimulateConnected(bool connected)
        {
            Connected = connected;
        }

        public void SimulateOverload(bool overload)
        {
            _overload = overload;
        }

        public void StartCharge()
        {
            // Starter opladning, hvis opladning ikke allerede er startet
            if (!_charging)
            {
                if (Connected && !_overload)
                {
                    CurrentValue = 500;
                }
                else if (Connected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!Connected)
                {
                    CurrentValue = 0.0;
                }

                OnUSBChanged();

                _ticksSinceStart = 0;
                _charging = true;
                _timer.Start(); 
            }
        }

        public void StopCharge()
        {
            _timer.Stop(); 

            CurrentValue = 0.0;
            OnUSBChanged();

            _charging = false;
        }

        private void OnUSBChanged()
        {
            USBChangedEvent?.Invoke(this, new USBChangedEventArgs() { Current = this.CurrentValue });
        }
    }
}
