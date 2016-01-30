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
    class Buffer<T>
    {
        CustomQueue<T> buffer;

        /// <summary>
        /// Instantiates a buffer with the default 25 blocks.
        /// </summary>
        public Buffer()
        {
        }
    }
}
