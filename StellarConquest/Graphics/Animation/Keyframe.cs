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
//###     Keyframe Ver. 0.001 Alpha                 ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   This is a normal keyframe. Contains delay time,
//###   and parameter changes like color, position, and
//###   what frame to select from the sprite sheet (sourceRectangle)
//###
//###   ~~~             USAGE               ~~~
//###
//###   Instead of instantiating this class, use either
//###   the included KeyframeList or
//###   KeyframeManager (<- Not yet implemented).
//###
//#####################################################

//#####################################################
//###
//###   Current progress        |       Percentage
//###
//###   Keyframe Implementation |       100%
//###   Helper Math             |       0%
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StellarConquest.Graphics.Animation
{
    /// <summary>
    /// Object for holding keyframe timing information.
    /// </summary>
    class Keyframe
    {
        public int Delay { get; protected set; }
        public Color Tint { get; protected set; }

        public Vector2 Position { get; protected set; }
        public Rectangle SourceRectangle { get; protected set; }

        public Keyframe(int delay, Color tint, Vector2 position, Rectangle sourceRectangle)
        {
            this.Delay = delay;
            this.Tint = tint;
            this.Position = position;
            this.SourceRectangle = sourceRectangle;
        }

        /// <summary>
        /// Creates a non-animating frame with a delay of 1000ms.
        /// </summary>
        public Keyframe()
        {
            this.Delay = 1000;
            this.Tint = Color.White;
            this.Position = new Vector2(0, 0);
            this.SourceRectangle = new Rectangle(0, 0, 0, 0);
        }
    }
}
