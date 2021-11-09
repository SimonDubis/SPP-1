using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerConsoleApp
{
    namespace OutputWriter
    {
        interface IOutputWriter
        {
            public void Write(string toWrite, StreamWriter writer = null);
        }
    }
}
