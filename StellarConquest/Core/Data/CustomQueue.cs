#region Liscense
/*

The MIT License (MIT)

Copyright (c) 2016 Udsahn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
#endregion

//#####################################################
//###                                               ###
//###       Custom Queue 0.001 Alpha                ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   The custom queue is essentially a non-resizing
//###   Queue. This different implementation is used to
//###   maintain a static size, but still hold data.
//###
//###   Care needs to be taken as if one tries to take
//###   data out of the queue that's empty it will result
//###   in an error.
//###
//###   The CustomQueue is used within internal programmed
//###   buffers to ensure a static size.
//###
//#####################################################

#endregion

#region Logic Scenario Diagram

/*
    ## Logic scenarios ##

    Can always assume that the newest index will be in front of the oldest
    index.

    New Queue________ 
    -|-|-|-|-|-|-|-|  <- Maximum capacity
    N
    O

    First addition [OverwriteLogic Method]
    =|-|-|-|-|-|-|-|  <- Maximum capacity
    O N

    Next insert index moves to front (0) [QueueLogic Method]
    =|=|=|=|=|=|=|=|  <- Maximum capacity
    O               N
    N<--------------|

    Oldest index moves to front (0) [QueueLogic Method]
    =|=|=|=|=|=|=|=|  <- Maximum capacity
    N               
    ----------------O
    O<--------------|


    Next insert overwrote oldest index. [OverwriteLogic Method]
    Queue length remains unchanged, oldest index steps forward.
    =|=|=|=|=|=|=|=|  <- Maximum capacity
    --------->N -----
    ------->O
            |>O
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StellarConquest.Core.Data
{
    /// <summary>
    /// A non-resizing generic queue. Has a maximum capacity.
    /// </summary>
    /// <typeparam name="T">Type to queue.</typeparam>
    class CustomQueue<T>
    {
        /// <summary>
        /// Queue container.
        /// </summary>
        protected T[] buckets;

        /// <summary>
        /// Next index to insert at.
        /// </summary>
        protected int _nextIndex;

        /// <summary>
        /// Next index to extract from.
        /// </summary>
        protected int _oldestIndex;

        protected bool _nextIndexLooped = false;

        /// <summary>
        /// Length of the queue.
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// Maximum capacity of the queue.
        /// </summary>
        public int Capacity { get; protected set; }

        #region ####    CONSTRUCTORS    ####

        /// <summary>
        /// Instantiates the queue with a default maximum capacity of 25 blocks.
        /// </summary>
        public CustomQueue()
        {
            Initialize(25);
        }

        /// <summary>
        /// Instantiates the queue with a custom maximum capacity.
        /// </summary>
        /// <param name="maximumCapacity"></param>
        public CustomQueue(int maximumCapacity)
        {
            Initialize(maximumCapacity);
        }

        /// <summary>
        /// Initializes the queue.
        /// </summary>
        /// <param name="maximumCapacity"></param>
        private void Initialize(int maximumCapacity)
        {
            // Ensure input was a legitimate value.
            if (maximumCapacity > 0)
                this.Capacity = maximumCapacity;
            else
                this.Capacity = 25;

            this.buckets = new T[maximumCapacity];
        }

        #endregion

        #region ####    METHODS     ####

        /// <summary>
        /// Adds to the queue.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public virtual void Enqueue(T item)
        {
            // Pre-check to see if the queue is full.
            if (_nextIndex >= buckets.Length)
                QueueLogic();

            // Overwrite if at full capacity.
            if (Length == Capacity)
            {
                OverwriteLogic(item);
                
                return;
            }
            else
                buckets[_nextIndex] = item;

            // Move the index to the next index.
            _nextIndex = Step(_nextIndex);

            // Increate the length of the queue.
            if(Length < buckets.Length)
                Length++;
        }
        
        /// <summary>
        /// Removes from the queue.
        /// </summary>
        /// <returns>Returns an item if present, or throws exception if empty.</returns>
        public virtual T Dequeue()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            // Pre-check to see if oldest index is out of range.
            if (_oldestIndex >= buckets.Length)
                QueueLogic();

            // Get oldest value.
            T value = buckets[_oldestIndex];
            _oldestIndex = Step(_oldestIndex);

            // Decrease the length of the queue.
            if (Length != 0)
                Length--;

            return value;
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            Clear(Capacity);
        }

        /// <summary>
        /// Resets the queue with a new maximum capacity.
        /// </summary>
        /// <param name="maximumCapacity">New maximum capacity.</param>
        public void Clear(int maximumCapacity)
        {
            Initialize(maximumCapacity);

            _nextIndex = 0;
            _oldestIndex = 0;
            Length = 0;
        }

        #endregion

        #region ####    NON-PUBLIC METHODS     ####

        /// <summary>
        /// Steps an index forward in the queue.
        /// </summary>
        /// <param name="indexToStep">Index to use as reference.</param>
        /// <returns>Returns resulting stepped index value.</returns>
        protected int Step(int indexToStep)
        {
            int value = 0;

            if (indexToStep < buckets.Length)
                value = indexToStep + 1;

            return value;
        }

        /*
            ## Logic scenarios ##

            Can always assume that the newest index will be in front of the oldest
            index.

            New Queue________ 
            -|-|-|-|-|-|-|-|  <- Maximum capacity
            N
            O

            First addition [OverwriteLogic Method]
            =|-|-|-|-|-|-|-|  <- Maximum capacity
            O N

            Next insert index moves to front (0) [QueueLogic Method]
            =|=|=|=|=|=|=|=|  <- Maximum capacity
            O               N
            N<--------------|
            
            Oldest index moves to front (0) [QueueLogic Method]
            =|=|=|=|=|=|=|=|  <- Maximum capacity
            N               
            ----------------O
            O<--------------|
            

            Next insert overwrote oldest index. [OverwriteLogic Method]
            Queue length remains unchanged, oldest index steps forward.
            =|=|=|=|=|=|=|=|  <- Maximum capacity
            --------->N -----
            ------->O
                    |>O
        */

        /// <summary>
        /// Overwrites oldest entry if queue is full.
        /// </summary>
        /// <param name="item">Item to overwrite with.</param>
        protected virtual void OverwriteLogic(T item)
        {
            _oldestIndex = Step(_oldestIndex);

            buckets[_nextIndex] = item;
        }

        /// <summary>
        /// Ensures that the indices selected never exceed the length of the array.
        /// </summary>
        protected virtual void QueueLogic()
        {
            if (_oldestIndex > _nextIndex)
            {
                if (_nextIndex == 0)
                    _oldestIndex = 0;
                else
                    _oldestIndex = _nextIndex - 1;
            }

            if (_nextIndex >= buckets.Length)
                _nextIndex = 0;
        }

        #endregion
    }
}
