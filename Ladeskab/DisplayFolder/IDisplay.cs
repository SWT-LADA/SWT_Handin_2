using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.DisplayFolder
{
    public interface IDisplay
    {
        void WriteMessage(string msg);
    }
}
