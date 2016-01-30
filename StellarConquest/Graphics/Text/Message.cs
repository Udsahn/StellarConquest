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
//###          Message Ver. 0.001 Alpha             ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   All text handling is used through this class.
//###
//###   Any message displayed to the screen can be
//###   animated in either translation or through
//###   coloration. All messages are handled by the
//###   MessageManager.
//###
//###   The Message class is used specifically for holding
//###   the message string, font desired and lifespan.
//###
//###   All actual rendering and output is handled
//###   via the MessageBox object.
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Udsahn.Graphics.FontLoader;

namespace StellarConquest.Graphics.Text
{
    class Message
    {
        public string Text { get; protected set; }
        public string Font { get; protected set; }
        
        /// <summary>
        /// Time displayed (in milliseconds)
        /// </summary>
        public int Lifespan { get; protected set; }

        /// <summary>
        /// The time elapsed (in milliseconds)
        /// </summary>
        public int Elapsed { get; protected set; }

        /// <summary>
        /// This is a flag to tell all managers to remove it.
        /// </summary>
        public bool IsAlive { get; protected set; }

        public Message(string message, string fontname = "DEFAULT", int lifespan = 1000, bool start = true)
        {
            this.Text = message;
            this.Font = fontname;
            this.Lifespan = lifespan;
            this.IsAlive = start;
        }

        public void Update(GameTime time)
        {
            if (Elapsed >= Lifespan)
                this.IsAlive = false;
            else
                this.Elapsed += time.ElapsedGameTime.Milliseconds;
        }
    }
}
