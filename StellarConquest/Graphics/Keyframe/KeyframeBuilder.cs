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
using System.Linq;
using System.Text;

namespace StellarConquest.Graphics.Keyframe
{

    /// <summary>
    /// Builds KeyframeLists and outputs with the property 'Result'
    /// </summary>
    class KeyframeBuilder : IDisposable
    {
        // Builds KeyframeLists.
        List<Keyframe> keyframes;

        /// <summary>
        /// Final output.
        /// </summary>
        public KeyframeList Result { get { return OutputKeyframeList(); } }

        /// <summary>
        ///  Number of keyframes.
        /// </summary>
        public int Count { get { return keyframes.Count; } }

        public KeyframeBuilder()
        {
            keyframes = new List<Keyframe>();
        }

        /// <summary>
        /// Includes an initial set of keyframes.
        /// </summary>
        /// <param name="frames">Frames to add.</param>
        public KeyframeBuilder(List<Keyframe> frames)
        {
            // Sets an initial set of frames for appending to.
            keyframes = frames;
        }

        /// <summary>
        /// Includes the keyframes from an already existing KeyframeList.
        /// </summary>
        /// <param name="frames">Frames to add.</param>
        public KeyframeBuilder(KeyframeList frames)
        {
            // Sets an initial set of frames for appending to.
            keyframes = frames.GetKeyframes();
        }

        /// <summary>
        /// Add a Keyframe.
        /// </summary>
        /// <param name="frame">Keyframe to add.</param>
        public void AddKeyframe(Keyframe frame)
        {
            keyframes.Add(frame);
        }

        /// <summary>
        /// Empties the list of keyframes.
        /// </summary>
        public void Clear()
        {
            keyframes.Clear();
        }

        /// <summary>
        /// Outputs a final trimmed result of keyframes as a KeyframeList.
        /// </summary>
        /// <returns>The resulting KeyframeList.</returns>
        private KeyframeList OutputKeyframeList()
        {
            var value = keyframes;
            value.TrimExcess();

            KeyframeList kList = new KeyframeList();
            kList.AddKeyframe(value);

            return kList;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~KeyframeBuilder() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
