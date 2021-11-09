using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    public class TraceResult
    {
        [Serializable]
        public class Thread
        {
            [Serializable]
            public class Method
            {
                public string Name { get; private set; }
                public string Class { get; private set; }
                public int Time;
                public Method[] Methods { get; private set; }

                public Method(MethodTrace currentMethodTrace)
                {
                    Name = currentMethodTrace.MethodName;
                    Class = currentMethodTrace.ClassName;
                    Time = currentMethodTrace.ExecutionTime;
                    Methods = formMethodsArray(currentMethodTrace.Methods.ToArray());
                }
                internal static Method[] formMethodsArray(MethodTrace[] methodTraceArray)
                {
                    Method[] methods = new Method[methodTraceArray.Length];

                    for (int i = 0; i < methods.Length; i++)
                    {
                        methods[i] = new Method(methodTraceArray[i]);
                    }
                    return methods;
                }
            }

            public int Id { get; private set; }
            public int Time { get; private set; }
            public Method[] Methods { get; private set; }

            public Thread(ThreadTrace currentThreadTrace)
            {
                Id = currentThreadTrace.Id;
                Time = currentThreadTrace.TotalExecutionTime;
                Methods = Method.formMethodsArray(currentThreadTrace.Methods.ToArray());
            }
        }

        public Thread[] threads { get; private set; }

        public TraceResult(Trace trace)
        {
            threads = new Thread[trace.threads.Count];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(trace.threads.ElementAt(i).Value);            
            }
        }
    }
}
