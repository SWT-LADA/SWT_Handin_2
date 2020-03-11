using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.RFIDEvent
{
    public interface IRFIDReader
    {
        event EventHandler<RFIDChangedEventArgs> RFIDChangedEvent;
    }
}
