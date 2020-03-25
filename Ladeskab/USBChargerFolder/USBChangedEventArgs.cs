using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.USBChargerFolder
{
    public class USBChangedEventArgs : EventArgs
    {
        public double Current { set; get; }        
    }
}
