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
//###       MessageBox Ver. 0.001 Alpha             ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   MessageBoxes are essentially containers for
//###   message objects. They handle the logic for if
//###   a message is displayed, how many can be on the
//###   screen at once, and the bounds within which a
//###   message is shown.
//###
//###   There are several settings for how the MessageBox
//###   handles new entries. It will either overwrite older
//###   entries or hold them in a buffer util such time
//###   that they can be shown.
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StellarConquest.Graphics.Animation;
using StellarConquest.Graphics.Text;

using Udsahn.Graphics.FontLoader;

namespace StellarConquest.Graphics.Text
{
    enum MessageBufferState { Clear, Store }
    enum MessageOverwriteState { Replace, Wait }

    class MessageBox
    {
        Message[] messages;
        LinkedList<Message> buffer;

        KeyframeManager km;

        // Indices for the Message array.
        // Used when overwriting oldest entries.
        int next, current, previous;

        /// <summary>
        /// Clear the buffer every update or store for next update.
        /// </summary>
        public MessageBufferState Buffer { get; set; }

        /// <summary>
        /// Overwrite oldest message or wait for message to expire.
        /// </summary>
        public MessageOverwriteState Overwrite { get; set; }

        public MessageBox(int capacity)
        {
            messages = new Message[capacity];
            buffer = new LinkedList<Message>();
            km = new KeyframeManager();

            this.Buffer = MessageBufferState.Store;
            this.Overwrite = MessageOverwriteState.Wait;
        }

        public void Update(GameTime time)
        {
            // Buffer Cleanup
            if(buffer.Count > 0)
            {
                int count = buffer.Count;

                // If Overwriting, replace oldest known index.
                if(Overwrite == MessageOverwriteState.Replace)
                {
                    // Replace up to the size of the array only.
                    for (int i = 0; i < count; i++)
                    {
                        messages[i] = buffer.First.Value;
                        buffer.RemoveFirst();
                    }

                    if (Buffer == MessageBufferState.Clear)
                        buffer.Clear();
                }

                if(Buffer == MessageBufferState.Clear)
                buffer.Clear();
            }

            for(int i = 0; i < messages.Length; i++)
            {
                if (messages[i] == null)
                    continue;

                if (messages[i].IsAlive)
                {
                    messages[i].Update(time);
                    km[i].Update(time);

                    if(!messages[i].IsAlive && buffer.Count != 0)
                    {
                        messages[i] = buffer.First.Value;
                        buffer.RemoveFirst();
                    }
                }

                // If the message is dead.
                else if (!messages[i].IsAlive && buffer.Count > 0)
                {
                    // Replace with what's in the buffer.
                    messages[i] = buffer.First.Value;
                    buffer.RemoveFirst();
                }
            }
        }

        public void Add(Message message, Vector2 position)
        {
            using (KeyframeBuilder kb = new KeyframeBuilder())
            {
                kb.AddKeyframe(new Keyframe(1000, Color.Wheat, position, new Rectangle(0, 0, 0, 0)));
                kb.AddKeyframe(new Keyframe(250, Color.White, position, new Rectangle(0, 0, 0, 0)));

                km.Add(kb.Result, next++.ToString());
                km[km.Count - 1].Start();
            }

            if(messages[current] == null)
            {
                messages[current] = message;

            if (++current >= messages.Length)
                    current = 0;

                return;
            }

            buffer.AddLast(message);
        }

        public void Draw(SpriteBatch sb, Font fontsheet)
        {
            for(int i = 0; i < messages.Length; i++)
            {
                if (messages[i] == null || !messages[i].IsAlive)
                    continue;

                Message text = messages[i];
                Keyframe frame = km[i].Keyframe;

                fontsheet.DrawText(text.Text + " | " + text.Elapsed, frame.Position, sb, frame.Tint);
            }
        }
        
    }
}
