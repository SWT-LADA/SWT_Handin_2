using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.RFIDEvent
{
    public class RFIDReader : IRFIDReader
    {
        private int _oldID;

        public event EventHandler<RFIDChangedEventArgs> RFIDChangedEvent;

        public void SetRFID(int ID) //hvordan testes den når den er void? 
        {
            if (ID !=_oldID)
            {
                OnRFIDChanged(new RFIDChangedEventArgs {RFID = ID});
                _oldID = ID; 
            }
        }

        protected virtual void OnRFIDChanged(RFIDChangedEventArgs e)
        {
            RFIDChangedEvent?.Invoke(this, e);
        }
    }
}
