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
//###     MonoGame Font Manager Ver. 0.001 Alpha    ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   Font Manager is a simple way to use multiple
//###   fonts at once. It contains a name tag for
//###   each provided font as well as indexed lookup.
//###
//###   This allows for easy printing to screen and
//###   identification.
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

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Udsahn.Graphics.FontLoader
{
    class FontManager
    {
        protected Dictionary<string, Font> fontByTag;
        protected List<string> fontByIndex;

        public FontManager()
        {
            fontByTag = new Dictionary<string, Font>();
            fontByIndex = new List<string>();
        }

        /// <summary>
        /// Adds a monospaced font into the manager database. Case sensitive.
        /// </summary>
        /// <param name="image">The image for the fontsheet.</param>
        /// <param name="tag">The stringed tag to give this sheet.</param>
        /// <param name="width">The width of each character.</param>
        /// <param name="height">The height of each character.</param>
        /// <param name="isFullSheet">False if only Capital characters present.</param>
        public void AddFontsheet(string tag, Texture2D image, int width, int height, bool isFullSheet)
        {
            string tagName = tag;

            if (!fontByTag.ContainsKey(tagName))
            {
                fontByTag.Add(tag, new Font(tagName, image, width, height, isFullSheet));
                fontByIndex.Add(tagName);
            }
        }

        /// <summary>
        /// Add an already instantiated monospaced fontsheet.
        /// </summary>
        /// <param name="fontsheet">The pre-instantiated fontsheet to add.</param>
        public void AddFontsheet(Font fontsheet)
        {
            string tagName = fontsheet.Name;

            if (!fontByTag.ContainsKey(tagName))
            {
                fontByTag.Add(tagName, fontsheet);
                fontByIndex.Add(tagName);
            }
        }

        //########### DRAW METHODS #############

        public void DrawText(string fontTag, string text, Vector2 position, SpriteBatch sb, Color tint)
        {
            Font fontsheet;

            if(fontByTag.TryGetValue(fontTag, out fontsheet))
                fontsheet.DrawText(text, position, sb, tint);
        }

        public void DrawText(int fontIndex, string text, Vector2 position, SpriteBatch sb, Color tint)
        {
            Font fontsheet;

            if (fontByIndex.Count > fontIndex && fontIndex > -1)
            {
                fontsheet = fontByTag[fontByIndex[fontIndex]];
                fontsheet.DrawText(text, position, sb, tint);
            }
        }

        //########### HELPER METHODS #############

        public int FindIndex(string fontTag)
        {
            return fontByIndex.IndexOf(fontTag);
        }

        public string FindTag(int fontIndex)
        {
            if (fontByIndex.Count > fontIndex && fontIndex > -1)
                return fontByIndex[fontIndex];
            else
                return string.Empty;
        }

    }
}
