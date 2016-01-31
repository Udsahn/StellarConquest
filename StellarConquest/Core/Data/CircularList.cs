using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StellarConquest.Core.Data
{
    /// <summary>
    /// A generic non-resizing container. A kind of non-growing LinkedList.
    /// </summary>
    /// <typeparam name="T">Type of object to store.</typeparam>
    class CircularList<T>
    {
        // Array for the list.
        protected T[] buckets;

        /// <summary>
        /// Returns the capacity of the internal array.
        /// </summary>
        public int Capacity { get { return buckets.Length; } }

        /// <summary>
        /// The number of current entries in the list.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Sets the list to either overwrite oldest entries or not when full.
        /// </summary>
        public bool Overwrite { get; protected set; }

        public T this[int index] { get { return buckets[index]; } }

        protected int _oldestIndex;
        protected int _nextIndex;

        #region ####    CONSTRUCTORS    ####

        public CircularList(int capacity = 10, bool overwrite = false)
        {
            this.Overwrite = overwrite;

            buckets = new T[capacity];
        }

        #endregion

        #region ####    METHODS     ####

        /// <summary>
        /// Adds a new item to the list.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>True if succeed.</returns>
        public virtual bool Include(T item)
        {
            // Pre-check if next insetion index is out of range to the array.
            if (_nextIndex >= Capacity)
                IndexLogic();

            if (Count == Capacity)
            {
                // If overwriting is enabled, overwrite and return true.
                if (OverwriteLogic(item))
                    return true;

                // Unable to add to the list.
                return false;
            }
            else
                buckets[_nextIndex] = item;

            // Move the index to the next insertion point.
            _nextIndex = Step(_nextIndex);

            if (Count < Capacity)
                Count++;

            return true;
        }

        /// <summary>
        /// Removes the oldest item from the list.
        /// </summary>
        /// <returns>True if succeed</returns>
        public virtual bool Reduce()
        {
            // Ensure there's something to remove.
            if (Count == 0)
                return false;

            _oldestIndex = Step(_oldestIndex);
            Count--;

            return true;
        }

        /// <summary>
        /// resizes the list. Ensures that no current entries are removed.
        /// </summary>
        /// <param name="capacity">New size.</param>
        public virtual void Resize(int capacity)
        {
            if (capacity < Count)
                return;
            
            T[] value = new T[capacity];

            // Add the items to the array
            for (int i = 0; i < Count; i++)
            {
                value[i] = buckets[_oldestIndex];
                _oldestIndex = Step(_oldestIndex);
            }

            // resets the indices in their new positions.
            _oldestIndex = 0;
            _nextIndex = Count;

            buckets = value;
        }

        /// <summary>
        /// Sorts the items in the list. Oldest to newest.
        /// </summary>
        public virtual void Sort()
        {
            // If empty, do nothing.
            if (Count == 0)
                return;

            T[] value = new T[Capacity];

            // Add the items to the array
            for (int i = 0; i < value.Length; i++)
            {
                if (_oldestIndex == value.Length)
                    _oldestIndex = 0;

                value[i] = buckets[_oldestIndex];


                if (_oldestIndex < value.Length)
                    _oldestIndex++;
            }

            // Final result.
            buckets = value;

            // resets the indices in their new positions.
            _oldestIndex = 0;
            _nextIndex = Count + 1;
        }

        #endregion

        #region ####    LOGIC   ####

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

        protected virtual void IndexLogic()
        {
            // Checks to see if the oldest index is in front of
            // the next insertion index.
            if (_oldestIndex > _nextIndex)
            {
                // If the next insertion is at the front, then
                // place the oldest at the same location.
                if (_nextIndex == 0)
                    _oldestIndex = 0;
                else
                    // Otherwise place the oldest index behind
                    // the next insertion index.
                    _oldestIndex = _nextIndex - 1;
            }

            // Resets the position of the next insertion index.
            if (_nextIndex >= Capacity)
                _nextIndex = 0;
        }

        protected virtual bool OverwriteLogic(T item)
        {
            buckets[_nextIndex] = item;

            _nextIndex = Step(_nextIndex);
            _oldestIndex = Step(_oldestIndex);

            return true;
        }

        #endregion
    }
}
