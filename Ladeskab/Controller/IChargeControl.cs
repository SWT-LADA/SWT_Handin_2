using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Controller
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();

        bool _IsConnected { get; set; }

    }
}
