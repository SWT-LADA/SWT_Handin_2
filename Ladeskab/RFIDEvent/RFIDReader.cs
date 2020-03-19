using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.RFIDEvent
{
    public class RFIDReader : IRFIDReader
    {

        public event EventHandler<RFIDChangedEventArgs> RFIDChangedEvent;

        public void SetRFID(int ID) //hvordan testes den når den er void? 
        { 
            OnRFIDChanged(new RFIDChangedEventArgs {RFID = ID});
        }

        protected virtual void OnRFIDChanged(RFIDChangedEventArgs e)
        {
            RFIDChangedEvent?.Invoke(this, e);
        }
    }
}
