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
//###       Generic Buffer. 0.001 Alpha             ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   To implement better buffering, a custom object
//###   needed. This generic, strongly typed buffer holds
//###   information that can be retrieved at a later time.
//###
//###   This is extremely useful for instances where a
//###   certain number of objects need to be instantiated
//###   to a certain capacity with extra instances being
//###   held in memory.
//###
//###   A prime example would be the TextBox which can only
//###   display a certain number of messages at a time.
//###
//###   Because the Generic Object Queue<T> is a potentially
//###   resizing array, it was decided to create a custom
//###   implementation of a non-resizing queue.
//###
//###   This custom queue would have a 'maximum' capacity that
//###   when reached would either 'replace' or 'ignore' further
//###   additions dependant on setting.
//###
//###   ~~~             USAGE               ~~~
//###
//###   After instantiation one simply adds to the buffer
//###   where necessary, and retrieves that information
//###   at a later time.
//###
//###   Upon this retrieval the buffer clears that block
//###   of information for the next.
//###
//###   The main method to retrieve this held data is the
//###   TryGetValue or TryGetValueRange method.
//###
//###   This style of implementation allows objects calling
//###   the these methods to use 'if' statement logic for
//###   extra syntactic sugar.
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StellarConquest.Core.Data
{
    /// <summary>
    /// Generic buffer for storing and retrieving items in a queue.
    /// </summary>
    /// <typeparam name="T">Type of object to buffer.</typeparam>
    class Buffer<T> : CircularQueue<T>
    {
        /// <summary>
        /// True to overwrite oldest entries.
        /// </summary>
        public bool Overwrite { get; set; }

        #region ####    CONSTRUCTORS    ####

        /// <summary>
        /// Instantiates a buffer with the default 25 blocks.
        /// </summary>
        /// <param name="overwrite">True to overwrite older entries when buffer full.</param>
        public Buffer(bool overwrite) : base(25) { this.Overwrite = overwrite; }

        /// <summary>
        /// Instantiates a buffer with a set maximum capacity.
        /// </summary>
        /// <param name="overwrite">True to overwrite older entries when buffer full.</param>
        /// <param name="maximumCapacity">Desired maximum capacity.</param>
        public Buffer(bool overwrite, int maximumCapacity) : base(maximumCapacity) { this.Overwrite = overwrite; }

        #endregion

        #region ####    METHODS     ####
        
        /// <summary>
        /// Attempts to pull from the queue. Empty if false.
        /// </summary>
        /// <param name="value">Object to send entry to.</param>
        /// <returns>True if successful -- False if fails.</returns>
        public bool TryDequeue(out T value)
        {
            // Unable to pull from buffer.
            if (Length == 0)
            {
                value = default(T);
                return false;
            }

            // Pre-check to see if oldest index is out of range.
            if (_oldestIndex >= buckets.Length)
                QueueLogic();

            // Get oldest item entry.
            T item = buckets[_oldestIndex];
            _oldestIndex = Step(_oldestIndex);

            // Decrease the length of the buffer.
            if (Length != 0)
                Length--;

            // Send result.
            value = item;
            return true;
        }

        /// <summary>
        /// Extracts all present entries from the buffer.
        /// </summary>
        /// <param name="values">Array to send the entries to.</param>
        /// <returns></returns>
        public bool TryDequeueRange(out T[] values)
        {
            T[] items;

            // Properly sizes the array.
            items = new T[Length];

            for (int i = 0; i < items.Length; i++)
            {
                T item;

                if (TryDequeue(out item))
                    items[i] = item;
            }

            values = items;
            return true;
        }

        /// <summary>
        /// Extracts a specific number of entries. If count is greater than buffer length, will fail.
        /// </summary>
        /// <param name="count">Desired number of entries.</param>
        /// <param name="values">Array object to send these entries to.</param>
        /// <returns></returns>
        public bool TryDequeueRange(int count, out T[] values)
        {
            // Unable to pull from buffer.
            if (Length == 0 || count > Length)
            {
                values = default(T[]);
                return false;
            }

            T[] items;

            // Properly sizes the array.
            items = new T[count];

            for (int i = 0; i < items.Length; i++)
            {
                T item;

                if (TryDequeue(out item))
                    items[i] = item;
            }

            values = items;
            return true;
        }

        #endregion

        #region ####    NON-PUBLIC METHODS      ####

        protected override void OverwriteLogic(T item)
        {
            if (Overwrite)
                base.OverwriteLogic(item);
        }

        #endregion
    }
}
