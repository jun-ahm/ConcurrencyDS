using System;
using Queue.Common;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace Queue.Client
{
    public static int Main()
    {
        List<IQueue<Job>> queues = new List<IQueue<Job>>()
        {
            new Queue.Locking.PriorityQueue<Job>(),
            new Queue.ReadWriteLock.PriorityQueue<Job>(),
        };

        foreach (IQueue<Job> queue in queues)
        {
            Console.WriteLine("Starting {0}.{1}", queue.GetType().Namespace, queue.GetType().Name);
            JobProcessor processor = new JobProcessor(queue);
            processor.Load(500000);

            Stopwatch processTimer = Stopwatch.StartNew();

            //start the concurrent job processing
            processor.ProcessInParallel(4);

            while(processor.Remaining > 0)
            {
                Console.WriteLine($"{processor.Remaining} : {processor.Completed}");
                Thread.Sleep(1000);
            }
            processor.Wait();
            processTimer.Stop();
            if(processor.Exception != null)
            {
                Console.WriteLine("Error {0}", processor.Exception.Message);               
            }
            else
            {
                Console.WriteLine("Success Process: {0}ms", processTimer.ElapsedMilliseconds);
            }
        }
        return 0;
    }
}