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

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace StellarConquest.Graphics.Animation
{
    class KeyframeManager
    {
        //  ### List of needed implementation ###
        //
        //  1.  List of KeyframeList to hold KeyframeSets
        //      This may need to have both an index and tag
        //      So that objects can refer to it in either
        //      manner.
        //
        //  2.  Update method(s) to update the internal logic
        //      on each KeyframeList

        protected readonly Dictionary<string, KeyframeList> keyframeSets;
        protected readonly List<string> keyframeSetTags;

        public KeyframeList this[string tag] { get { return Find(tag); } }
        public KeyframeList this[int index] { get { return Find(Find(index)); } }

        /// <summary>
        /// The number of current keyframes.
        /// </summary>
        public int Count { get { return keyframeSets.Count; } }

        public KeyframeManager()
        {
            keyframeSets = new Dictionary<string, KeyframeList>(25);
            keyframeSetTags = new List<string>(25);
        }

        public void Update(GameTime time)
        {
            // Updates every keyframe list.
            foreach (KeyframeList kl in keyframeSets.Values)
            {
                kl.Update(time);
            }
        }

        /// <summary>
        /// Starts all the keyframes updating.
        /// </summary>
        public void StartAll()
        {
            foreach (KeyframeList kl in keyframeSets.Values)
            {
                kl.Start();
            }
        }

        /// <summary>
        /// Freezes all the keyframes from updating.
        /// </summary>
        public void StopAll()
        {
            foreach (KeyframeList kl in keyframeSets.Values)
            {
                kl.Stop();
            }
        }

        public void Add(KeyframeList keyframes, string tag)
        {
            keyframeSets.Add(tag, keyframes);
            keyframeSetTags.Add(tag);
        }

        /// <summary>
        /// Locates the keyframe tag from an index.
        /// </summary>
        /// <param name="index">Index to find tag of.</param>
        /// <returns>Returned tag.</returns>
        public string FindTag(int index)
        {
            if (keyframeSetTags.Count <= index && index >= 0)
                return keyframeSetTags[index];

            return string.Empty;
        }

        // Getter methods.
        private KeyframeList Find(string tag)
        {
            KeyframeList kList;

            if(keyframeSets.TryGetValue(tag, out kList))
                return kList;

            return null;
        }

        private string Find(int index)
        {
            string tag = keyframeSetTags[index];

            return tag;
        }
    }



}
