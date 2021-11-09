using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Tracer
{
    public class Trace
    {
        public ConcurrentDictionary<int, ThreadTrace> threads;

        public Trace()
        {
            threads = new ConcurrentDictionary<int, ThreadTrace>();
        }

        public void StartMethodTrace(MethodTrace method)
        {
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            ThreadTrace currentThreadTrace;
            if (threads.TryGetValue(currentThreadID, out currentThreadTrace))
            {
                currentThreadTrace.StartMethodTrace(method);
            }
            else
            {
                ThreadTrace newThreadTrace = new ThreadTrace(currentThreadID);
                if (threads.TryAdd(currentThreadID, newThreadTrace))
                {
                    newThreadTrace.StartMethodTrace(method);
                }
                else
                {
                    throw new Exception("Error occured while trying to add thread");
                }
            }
        }

        public void EndLastMethodTrace()
        {
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            ThreadTrace currentThreadTrace;
            if (threads.TryGetValue(currentThreadID, out currentThreadTrace))
            {
                currentThreadTrace.StopMethodTrace();
            }
            else
            {
                throw new Exception("Error occured while trying to stop tracing method. Current thread arent tracing");
            }
        }
    }
}
