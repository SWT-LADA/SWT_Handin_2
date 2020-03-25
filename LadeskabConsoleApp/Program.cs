using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Controller;
using Ladeskab.DisplayFolder;
using Ladeskab.DoorEvent;
using Ladeskab.FileWriterFolder;
using Ladeskab.RFIDEvent;
using Ladeskab.USBChargerFolder;

namespace LadeskabConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Door door = new Door();
            RFIDReader rfidReader = new RFIDReader();
            Display display = new Display();
            USBCharger usbCharger = new USBCharger(); 
            FileWriter fileWriter = new FileWriter();
            ChargeControl chargeControl = new ChargeControl(usbCharger, display); 
            StationControl stationControl = new StationControl(door, rfidReader, display, chargeControl, usbCharger, fileWriter); 

            System.Console.WriteLine("E = exit, O = open, C = close, R = read");
            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.SetDoorState(true);
                        break;

                    case 'C':
                        door.SetDoorState(false);
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString); 
                        rfidReader.SetRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
