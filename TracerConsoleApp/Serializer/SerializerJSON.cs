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
using TracerConsoleApp.Serializer;

namespace TracerConsoleApp
{
    namespace Serializer
    {
        public class SerializerJSON : ISerializer
        {
            public string Serialize(TraceResult traceResult)
            {
                string output;
                JsonSerializer serializer = new JsonSerializer();

                output = JsonConvert.SerializeObject(traceResult, Formatting.Indented);

                return output;
            }

        }
    }
}
