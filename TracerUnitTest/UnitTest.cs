using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer;

namespace TracerUnitTest
{
    [TestClass]
    public class TracerLibTests
    {

        public Tracer.Tracer Tracer = new Tracer.Tracer();

        private readonly List<Thread> threads = new List<Thread>();

        int ThreadsCount = 5;
        int MethodsCount = 5;

        int MillisecondsTimeout = 100;

        private void Method()
        {
            Tracer.StartTrace();
            Thread.Sleep(MillisecondsTimeout);
            Tracer.StopTrace();
        }

        [TestMethod]
        public void ExecutionTime()
        {
            Method();
            TraceResult traceResult = Tracer.GetTraceResult();
            double methodTime = traceResult.threads[0].Methods[0].Time;
            double threadTime = traceResult.threads[0].Time;
            Assert.IsTrue(methodTime >= MillisecondsTimeout);
            Assert.IsTrue(threadTime >= MillisecondsTimeout);
        }

        [TestMethod]
        public void ThreadCount()
        {
            for (int i = 0; i < ThreadsCount; i++)
            {
                threads.Add(new Thread(Method));
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
                thread.Join();
            }

            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(ThreadsCount, traceResult.threads.Length);
        }

        [TestMethod]
        public void MethodCount()
        {
            for (int i = 0; i < MethodsCount; i++)
            {
                Method();
            }

            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(MethodsCount, traceResult.threads[0].Methods.Length);
        }

        [TestMethod]
        public void Name()
        {
            Tracer.StartTrace();
            Tracer.StopTrace();
            TraceResult traceResult = Tracer.GetTraceResult();
            Assert.AreEqual(nameof(Name), traceResult.threads[0].Methods[0].Name);
            Assert.AreEqual(nameof(TracerLibTests), traceResult.threads[0].Methods[0].Class);
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, traceResult.threads[0].Id);
        }
    }
}
