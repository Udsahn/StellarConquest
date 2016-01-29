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
//###     MonoGame Font Loader Ver. 0.005 Alpha     ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###
//###   ~~~             ABOUT               ~~~
//###
//### What follows is sample code for a font loader
//### using any image MonoGame can import as a Texture2D.
//###
//### The font is very simplistic and includes no kerning.
//###
//### NOTE: This is a sample, and the published version
//###       be a class library.
//###
//###   ~~~             USAGE               ~~~
//###
//### Upon instantiation of the fontloader, a Texture2D
//### must be sent as a parameter to load as well as a
//### Vector2 for the size of each character as well as
//### another Vector2 for any offset between characters.
//###
//### An overload is included to specify the origin.
//### 
//### At default, the origin is set at 0, 0.
//###
//### By default the Fontloader imports in the following
//### manner by each row:
//### 
//### Row #     |   Type
//### 1.        |   Capital Letters
//### 2.        |   Lowercase Letters
//### 3.        |   Numbers
//### 4.        |   Punctuation
//### 5.        |   Special Characters (Empty space, Filled space/'Unknown' and underscore)
//###
//### Future versions will include .XML implimentation
//### for more acurate placement and letters represented.
//###
//#####################################################

//#####################################################
//###
//### Current progress          |       Percentage
//###
//###   Font Loading            |           100%
//###   Font Rendering          |           100%
//###   Font XML Info           |           0%
//###   Scaling                 |           0%
//###
//###   Letters Supported       |       Most Characters
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Udsahn.Graphics.FontLoader
{
    class Font
    {
        public Texture2D FontSheet { get; protected set; }
        public string Name { get; protected set; }

        public Rectangle TypeSize { get; protected set; }

        private FontRenderer _fontRenderer;

        //########### Constructors #############

        public Font(string name, Texture2D fontImage, Vector2 characterSize, bool hasFullSet = false)
        {
            this.Name = name;
            this.FontSheet = fontImage;
            this.TypeSize = new Rectangle(0, 0, (int)Math.Round(characterSize.X), (int)Math.Round(characterSize.Y));

            if (!hasFullSet)
                _fontRenderer = new FontRenderer(TypeSize, fontImage);
            else
                _fontRenderer = new FontRenderer(TypeSize, fontImage, true);
        }

        public Font(string name, Texture2D fontImage, int width, int height, bool hasFullSet = false)
        {
            this.Name = name;
            this.FontSheet = fontImage;
            this.TypeSize = new Rectangle(0, 0, width, height);

            if (!hasFullSet)
                _fontRenderer = new FontRenderer(TypeSize, fontImage);
            else
                _fontRenderer = new FontRenderer(TypeSize, fontImage, true);
        }

        //########### Methods #############

        public void Demo(Vector2 position, SpriteBatch sb)
        {
            DrawText("ABCDEFGHIJKLMNOPQRSTUVWXYZ", position, sb);
            DrawText("abcdefghijklmnopqrstuvwxyz", new Vector2(position.X, position.Y + TypeSize.Height), sb);
            DrawText("1234567890", new Vector2(position.X, position.Y + (TypeSize.Height * 2)), sb);
            DrawText(".!?,;", new Vector2(position.X, position.Y + (TypeSize.Height * 3)), sb);
            DrawText(" |_-", new Vector2(position.X, position.Y + (TypeSize.Height * 4)), sb);

        }

        public void DrawText(string text, Vector2 position, SpriteBatch sb)
        {
            _fontRenderer.Draw(text, (int)position.X, (int)position.Y, sb);
        }

        public void DrawText(string text, Vector2 position, SpriteBatch sb, Color tint)
        {
            _fontRenderer.Draw(text, (int)position.X, (int)position.Y, sb, tint);
        }

    }

    /// <summary>
    /// Main renderer for the fonts.
    /// </summary>
    public class FontRenderer
    {

        private Dictionary<char, FontCharacter> letters = new Dictionary<char, FontCharacter>();
        private Texture2D _fontSheet;

        //########### Constructors #############

        public FontRenderer(Rectangle typeSize, Texture2D fontSheet, bool isFullSet = false)
        {
            if (!isFullSet)
                Initialize(typeSize);
            else
                Initialize(typeSize, true);

            this._fontSheet = fontSheet;
        }

        //########### Character set Initializer #############

        private void Initialize(Rectangle typeSize, bool isFullSet = false)
        {
            #region Row 1 - Capital Letters
            letters.Add('A', new FontCharacter('A', 0, 0, typeSize.Width, typeSize.Height));
            letters.Add('B', new FontCharacter('B', 1 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('C', new FontCharacter('C', 2 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('D', new FontCharacter('D', 3 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('E', new FontCharacter('E', 4 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('F', new FontCharacter('F', 5 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('G', new FontCharacter('G', 6 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('H', new FontCharacter('H', 7 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('I', new FontCharacter('I', 8 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('J', new FontCharacter('J', 9 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('K', new FontCharacter('K', 10 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('L', new FontCharacter('L', 11 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('M', new FontCharacter('M', 12 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('N', new FontCharacter('N', 13 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('O', new FontCharacter('O', 14 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('P', new FontCharacter('P', 15 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('Q', new FontCharacter('Q', 16 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('R', new FontCharacter('R', 17 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('S', new FontCharacter('S', 18 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('T', new FontCharacter('T', 19 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('U', new FontCharacter('U', 20 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('V', new FontCharacter('V', 21 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('W', new FontCharacter('W', 22 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('X', new FontCharacter('X', 23 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('Y', new FontCharacter('Y', 24 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            letters.Add('Z', new FontCharacter('Z', 25 * typeSize.Width, 0, typeSize.Width, typeSize.Height));
            #endregion

            if (isFullSet)
            {
                #region Row 2 - Lowercase Letters
                letters.Add('a', new FontCharacter('a', 0, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('b', new FontCharacter('b', 1 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('c', new FontCharacter('c', 2 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('d', new FontCharacter('d', 3 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('e', new FontCharacter('e', 4 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('f', new FontCharacter('f', 5 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('g', new FontCharacter('g', 6 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('h', new FontCharacter('h', 7 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('i', new FontCharacter('i', 8 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('j', new FontCharacter('j', 9 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('k', new FontCharacter('k', 10 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('l', new FontCharacter('l', 11 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('m', new FontCharacter('m', 12 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('n', new FontCharacter('n', 13 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('o', new FontCharacter('o', 14 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('p', new FontCharacter('p', 15 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('q', new FontCharacter('q', 16 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('r', new FontCharacter('r', 17 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('s', new FontCharacter('s', 18 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('t', new FontCharacter('t', 19 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('u', new FontCharacter('u', 20 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('v', new FontCharacter('v', 21 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('w', new FontCharacter('w', 22 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('x', new FontCharacter('x', 23 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('y', new FontCharacter('y', 24 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('z', new FontCharacter('z', 25 * typeSize.Width, typeSize.Height, typeSize.Width, typeSize.Height));
                #endregion
                #region Row 3 - Numbers
                letters.Add('1', new FontCharacter('1', 0, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('2', new FontCharacter('2', 1 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('3', new FontCharacter('3', 2 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('4', new FontCharacter('4', 3 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('5', new FontCharacter('5', 4 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('6', new FontCharacter('6', 5 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('7', new FontCharacter('7', 6 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('8', new FontCharacter('8', 7 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('9', new FontCharacter('9', 8 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('0', new FontCharacter('0', 9 * typeSize.Width, 2 * typeSize.Height, typeSize.Width, typeSize.Height));
                #endregion
                #region Row 4 - Punctuation
                letters.Add('.', new FontCharacter('.', 0, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('!', new FontCharacter('!', 1 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('?', new FontCharacter('?', 2 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add(',', new FontCharacter(',', 3 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add(';', new FontCharacter(';', 4 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add(':', new FontCharacter(':', 5 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('<', new FontCharacter('<', 6 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('>', new FontCharacter('>', 7 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('-', new FontCharacter('-', 8 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('~', new FontCharacter('~', 9 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('(', new FontCharacter('(', 10 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add(')', new FontCharacter(')', 11 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('{', new FontCharacter('{', 12 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('}', new FontCharacter('}', 13 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('[', new FontCharacter('[', 14 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add(']', new FontCharacter(']', 15 * typeSize.Width, 3 * typeSize.Height, typeSize.Width, typeSize.Height));

                #endregion
                #region Row 5 - Special Characters
                letters.Add(' ', new FontCharacter(' ', 0, 4 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('|', new FontCharacter('|', 1 * typeSize.Width, 4 * typeSize.Height, typeSize.Width, typeSize.Height));
                letters.Add('_', new FontCharacter('_', 2 * typeSize.Width, 4 * typeSize.Height, typeSize.Width, typeSize.Height));
                #endregion
            }

        }

        //########### Drawing Methods #############

        public void Draw(string text, int x, int y, SpriteBatch sb)
        {
            int deltaX = x;
            int deltaY = y;

            foreach(char c in text)
            {
                FontCharacter fc;
                if (letters.TryGetValue(c, out fc))
                {
                    var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                    var position = new Vector2(deltaX, deltaY);

                    sb.Draw(_fontSheet, position, sourceRectangle, Color.White);

                    // Reposition the font by the width of the character.
                    deltaX += fc.Width;
                }
                else
                {
                    if (letters.ContainsKey('|'))
                    {
                        fc = letters['|'];

                        var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                        var position = new Vector2(deltaX, deltaY);

                        sb.Draw(_fontSheet, position, sourceRectangle, Color.White);

                        // Reposition the font by the width of the character.
                        deltaX += fc.Width;
                        continue;
                    }
                    deltaX += letters['A'].Width;
                }
            }
        }

        public void Draw(string text, int x, int y, SpriteBatch sb, Color tint)
        {
            int deltaX = x;
            int deltaY = y;

            foreach (char c in text)
            {
                FontCharacter fc;
                if (letters.TryGetValue(c, out fc))
                {
                    var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                    var position = new Vector2(deltaX, deltaY);

                    sb.Draw(_fontSheet, position, sourceRectangle, tint);

                    // Reposition the font by the width of the character.
                    deltaX += fc.Width;
                }
                else
                {
                    if (letters.ContainsKey('|'))
                    {
                        fc = letters['|'];

                        var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                        var position = new Vector2(deltaX, deltaY);

                        sb.Draw(_fontSheet, position, sourceRectangle, tint);

                        // Reposition the font by the width of the character.
                        deltaX += fc.Width;
                        continue;
                    }
                    deltaX += letters['A'].Width;
                }
            }
        }

    }

}
