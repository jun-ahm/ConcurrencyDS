using System;

namespace Queue.Common
{
    public interface IQueue<T>
    {
        //Adds an item to the back of the queue
        void Enqueue(T item);

        //Removes and return the front item from the queue
        T Dequeue();

        //Returns the front item from the queue without remoing it from the queue
        T Peek();

        //The number of items in the queue
        int Count
        {
            get;
        }

        //Removes all items from the queue
        void Clear();
           
    }
}