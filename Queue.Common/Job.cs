using System;
using System.Threading;

namespace Queue.Common
{
    public class Job : IComparable<Job>
    {
        //indicates the number of times Process has been called
        int performed = 0;

        readonly int Priority;

        public Job(int priority)
        {
            this.Priority = priority;
        }

        public int CompareTo(Job other)
        {
            return Priority.CompareTo(other?.Priority);
        }

        public void Process()
        {
            //If process is called more than once then throw an exception
            if(Interlocked.Increment(ref performed) > 1)
            {
                throw new InvalidOperationException("Action performed multiple times");
            }
        }
    }
}