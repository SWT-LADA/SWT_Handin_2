using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.FileWriterFolder
{
    public class FileWriter : IFileWriter
    {
        private string logFile = "logfile.txt";
        public void LogDoorLocked(int ID)
        {
            using (var writer = File.AppendText(logFile)) // Jeg er ikke sikker på hvor filen bliver gemt henne /Line
            {
                writer.WriteLine(DateTime.Now + ": Door locked with RFID: {0}", ID);
                Console.WriteLine("Døren er blevet låst"); // Skal slettes inden aflevering!! /Line
            }
        }

        public void LogDoorUnlocked(int ID)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Door unlocked with RFID: {0}", ID);
                Console.WriteLine("Døren er blevet låst op"); // Skal slettes inden aflevering!! /Line
            }
        }
    }
}
