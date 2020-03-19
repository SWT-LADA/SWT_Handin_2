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
            Display display = new Display(); // Tilføjet af Line 18/3 16.30
            USBCharger usbCharger = new USBCharger(); // Tilføjet af Line 18/3 16.30
            FileWriter fileWriter = new FileWriter(); // Tilføjet af Line 18/3 16.30
            ChargeControl chargeControl = new ChargeControl(usbCharger, display); // Tilføjet af Line 18/3 16.30
            StationControl stationControl = new StationControl(door, rfidReader, display, chargeControl, usbCharger, fileWriter); // Tilføjet af Line 18/3 16.30

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

                        int id = Convert.ToInt32(idString); // Her kommer en exception hvis man ikke indtaster et ID. Vi kan overveje om vi vil lave vores egen exception /Line
                        rfidReader.SetRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
