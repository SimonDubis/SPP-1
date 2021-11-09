using System;
using System.Collections.Concurrent;

namespace Tracer
{
    public class MethodTrace
    {
        public string MethodName;
        public string ClassName;
        public int ExecutionTime { get; private set; }

        private DateTime startTime;

        public ConcurrentBag<MethodTrace> Methods;

        public MethodTrace(string methodName, string className)
        {
            this.MethodName = methodName;
            this.ClassName = className;
            this.Methods = new ConcurrentBag<MethodTrace>();
            this.ExecutionTime = -1;
        }

        public void StartCount()
        {
            startTime = DateTime.UtcNow;
        }

        public void StopCount()
        {
            DateTime stopTime = DateTime.UtcNow;
            TimeSpan span = stopTime - startTime;
            ExecutionTime = (int)span.TotalMilliseconds;
        }
    }
}
