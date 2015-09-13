using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
//Kamal : Class description goes here , summarize whats the main purpose of this class

namespace RssReader
{
    /// <summary>
    /// Need a queue that is threadsafe. This should be that collection.
    /// //Kamal - ConcurrentDictionary could have been used that is also thread safe or inherit stack or create stack inside if need to be wrap around 
    /// </summary>
    public class ThreadSafeQueue 
    {
        /*~ Mutex ~*/
        private object _lock;

        private Stack _stack = null;

        /// <summary>
        /// Backing data store is constant.
        /// </summary>
        //private List<int> BACKING_LIST;
        /// <summary>
        /// Constructor
        /// </summary>
        public ThreadSafeQueue() {
            //BACKING_LIST = new List<int>();

            _stack = new Stack();
        }

        /// <summary>
        /// Add value to the queue.
        /// </summary>
        /// <param name="value">Float value to add</param>
        public void Enqueue(int value)
        {
            //BACKING_LIST.Add(value);
            _stack.Push(value);
            _lock = new object();
           
        }

        /// <summary>
        /// returns items count in queue
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _stack.Count;
        }

        /// <summary>
        /// Return and remove a value from the queue.
        /// </summary>
        /// <param name="defaultValue">Default to return if queue is empty.</param>
        /// <returns>Number.</returns>
        public int Pop()
        {
            int value; // Store an int
            //kamal : ReaderWriterLockSlim could have been used from performance perspective
            try {
                lock (_lock) { // Thread safety for the win
                    
                        // value = BACKING_LIST.Last(); // Take the leading value in the queue
                        value =(int) _stack.Pop();

                }

               // BACKING_LIST.Remove(value); // Make sure to remove that value.
                return value; // Return the value
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred in Pop method." + ex.Message);

            }
        }
    }
}
