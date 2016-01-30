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
//###     Keyframe List Ver. 0.004 Alpha            ###
//###                                               ###
//#####################################################
#region Info

//#####################################################
//###   ~~~             ABOUT               ~~~
//###
//###   This class implements a collection of and
//###   timing for a set of keyframes.
//###
//###   ~~~             USAGE               ~~~
//###
//###   Upon instantiation, pass a list of frames to
//###   include. Otherwise use one of the AddFrame Methods.
//###
//###   Ensure to set the Alive property to true to engage
//###   timing updates. Or else all keyframes will be frozen.
//###
//###   Aditional future options will include setting the state
//###   of the KeyframeList as well as having different looping
//###   styles.
//###
//#####################################################

//#####################################################
//###
//###   Current progress        |       Percentage
//###
//###   Keyframe List           |       100%
//###   Object State            |       100%
//###   Cyclic options          |       100%
//###
//#####################################################

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StellarConquest.Graphics.Keyframe
{
    enum KeyframeState { Alive, Frozen };
    enum KeyframeCycle { Repeat, Once, Reverse };

    class KeyframeList
    {
        protected List<Keyframe> keyframes;

        public int Elapsed { get; protected set; }
        private int _lastKeyframeIndex;
        private int _curentKeyframeIndex;

        public KeyframeState State { get; set; }
        public KeyframeCycle Cycle { get; set; }

        public Keyframe Keyframe { get { return keyframes[KeyframeIndex]; }  }

        public int KeyframeIndex { get { return _curentKeyframeIndex; } protected set { _curentKeyframeIndex = value; } }
        public int LastKeyframeIndex { get { return _lastKeyframeIndex; } protected set { _lastKeyframeIndex = value; } }

        public KeyframeList()
        {
            keyframes = new List<Keyframe>();
            State = KeyframeState.Frozen;
        }

        //########### UPDATE METHODS #############

        public void Update(GameTime time)
        {
            if (State == KeyframeState.Alive)
            {
                Elapsed += time.ElapsedGameTime.Milliseconds;

                if (Elapsed >= keyframes[KeyframeIndex].Delay)
                {
                    if (Cycle != KeyframeCycle.Reverse)
                        this.Next();
                    else
                        this.Previous();
                }
            }
        }

        public void Next()
        {
            if (KeyframeIndex >= keyframes.Count - 1)
            {
                if (Cycle == KeyframeCycle.Once)
                {
                    Stop();
                    return;
                }

                LastKeyframeIndex = KeyframeIndex;
                KeyframeIndex = 0;
            }
            else
            {
                LastKeyframeIndex = KeyframeIndex;
                KeyframeIndex++;
            }

            Elapsed = 0;
        }

        public void Previous()
        {
            if (Cycle != KeyframeCycle.Reverse)
            {
                if (KeyframeIndex <= 0)
                {
                    LastKeyframeIndex = KeyframeIndex;
                    KeyframeIndex = 0;
                }
                else
                {
                    LastKeyframeIndex = KeyframeIndex;
                    KeyframeIndex--;
                }
            }
            else // Reverse the keyframes.
            {
                if (KeyframeIndex <= 0)
                {
                    LastKeyframeIndex = KeyframeIndex;
                    KeyframeIndex = keyframes.Count - 1;
                }
                else
                {
                    LastKeyframeIndex = KeyframeIndex;
                    KeyframeIndex--;
                }
            }

            Elapsed = 0;
        }

        public void Start()
        {
            State = KeyframeState.Alive;
        }

        public void Stop()
        {
            State = KeyframeState.Frozen;
        }

        public void Reset()
        {
                Elapsed = 0;
                KeyframeIndex = 0;
        }

        public List<Keyframe> GetKeyframes()
        {
            List<Keyframe> value = this.keyframes;

            return value;
        }

        #region AddMethods
        //########### ADD KEYFRAMES #############

        public void AddKeyframe(List<Keyframe> frames)
        {
            if (keyframes.Count == 0)
                keyframes = frames;
            else
                foreach (Keyframe frame in frames)
                     keyframes.Add(frame);
        }

        public void AddKeyframe(int delay)
        {
            if (keyframes.Count > 0)
            {
                // Retrieve last keyframe for reference.
                Keyframe lastKeyframe = GetLastKeyframe();

                Color lastColor = lastKeyframe.Tint;
                Vector2 lastPosition = lastKeyframe.Position;
                Rectangle lastSourceRectangle = lastKeyframe.SourceRectangle;

                keyframes.Add(new Keyframe(delay, lastColor, lastPosition, lastSourceRectangle));
            }
            else
                keyframes.Add(new Keyframe(delay, Color.White, new Vector2(0, 0), new Rectangle(0, 0, 0, 0)));
        }

        public void AddKeyframe(int delay, Color color)
        {
            if (keyframes.Count > 0)
            {
                // Retrieve last keyframe for reference.
                Keyframe lastKeyframe = GetLastKeyframe();

                Vector2 lastPosition = lastKeyframe.Position;
                Rectangle lastSourceRectangle = lastKeyframe.SourceRectangle;

                keyframes.Add(new Keyframe(delay, color, lastPosition, lastSourceRectangle));
            }
            else
                keyframes.Add(new Keyframe(delay, color, new Vector2(0, 0), new Rectangle(0, 0, 0, 0)));
        }

        public void AddKeyframe(int delay, Color color, Vector2 position)
        {
            if (keyframes.Count > 0)
            {
                // Retrieve last keyframe for reference.
                Keyframe lastKeyframe = GetLastKeyframe();

                Rectangle lastSourceRectangle = lastKeyframe.SourceRectangle;

                keyframes.Add(new Keyframe(delay, color, position, lastSourceRectangle));
            }
            else
                keyframes.Add(new Keyframe(delay, color, position, new Rectangle(0, 0, 0, 0)));
        }

        public void AddKeyframe(int delay, Color color, Vector2 position, Rectangle sourceRectangle)
        {
            keyframes.Add(new Keyframe(delay, color, position, sourceRectangle));
        }
        #endregion

        private Keyframe GetLastKeyframe()
        {
            int lastKeyframeIndex = keyframes.Count - 1;
            Keyframe lastKeyframe = keyframes[lastKeyframeIndex];
            return lastKeyframe;
        }
    }
}
