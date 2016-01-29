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
//###     Text Message Ver. 0.001 Alpha             ###
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

//###
//###   ~~~             USAGE               ~~~
//###
//###   After instantiation, simply use the AddFontsheet
//###   method to include new fontsheets.
//###
//###   To access these fonts afterward use the DrawText
//###   method. Supplying either a string name or index
//###   number to select a font.
//###
//###   If no font was located, the manager will flash
//###   an error in the game window. This feature can
//###   be disabled by changing the boolean flag 'silent'
//###   to true.
//###
//###   A later feature will allow 'timed displays'
//###   that will retain a section of text for an
//###   amount of time.
//###
//###   An extra feature will also include the capability
//###   to shift the color of text over time, and offset
//###   its position (translate).
//#####################################################

//#####################################################
//###
//###   Current progress        |       Percentage
//###
//###   Font Manager            |       100%
//###   Debug Flash             |       0%
//###   Color shifting          |       0%
//###   Timed Translation       |       0%
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

namespace StellarConquest.Text
{
    class Message
    {
        public string Text { get; protected set; }
        public Color Color { get; protected set; }

        public bool IsAnimating { get; protected set; }

        List<int> timers;
        List<Color> colors;
        List<Vector2> positions;

        public Message(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }

        public void AddKeyframe(int time, Color color, Vector2 relativePosition)
        {
            if (timers == null)
            {
                timers = new List<int>();
                colors = new List<Color>();
                positions = new List<Vector2>();
            }

            int keyframe = MathHelper.Clamp(time, 1, 60000);

            timers.Add(MathHelper.Clamp(time, 1, 60000));
        }
    }
}
