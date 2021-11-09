using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private Trace trace;

        public Tracer()
        {
            trace = new Trace();
        }

        public void StartTrace()
        {
            //get method info using reflection
            StackTrace stackTrace = new StackTrace();
            MethodBase callingMethod = stackTrace.GetFrame(1).GetMethod();
            string methodName = callingMethod.Name;
            string className = callingMethod.ReflectedType.Name;

            MethodTrace currentMethodTrace = new MethodTrace(methodName, className);
            trace.StartMethodTrace(currentMethodTrace);
            currentMethodTrace.StartCount();
        }

        public void StopTrace()
        {
            trace.EndLastMethodTrace();
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(trace);
        }
    }
}
