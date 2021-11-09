using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Tracer;

namespace TracerConsoleApp
{
    namespace Serializer
    {
        public interface ISerializer
        {
            string Serialize(TraceResult traceResults);
        }
    }
}