﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.FileWriterFolder
{
    public interface IFileWriter
    {
        void LogDoorLocked(int ID);
        void LogDoorUnlocked(int ID);
    }
}
