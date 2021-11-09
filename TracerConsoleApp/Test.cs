using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Tracer;

namespace TracerConsoleApp
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod2()
        {
            _tracer.StartTrace();

            Thread.Sleep(50);

            _tracer.StopTrace();
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            InnerMethod2();
            Thread thread = new Thread(InnerMethod2);
            thread.Start();
            thread.Join();

            _tracer.StopTrace();
        }
    }
}
