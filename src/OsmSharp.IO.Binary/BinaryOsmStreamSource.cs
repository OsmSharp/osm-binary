// The MIT License (MIT)

// Copyright (c) 2017 Ben Abelshausen

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.IO;
using OsmSharp.IO.Binary;

namespace OsmSharp.Streams
{
    /// <summary>
    /// A stream source that just reads objects in binary format.
    /// </summary>
    public class BinaryOsmStreamSource : OsmStreamSource
    {
        private readonly Stream _stream;
        private readonly byte[] _buffer;

        /// <summary>
        /// Creates a new binary stream source.
        /// </summary>
        public BinaryOsmStreamSource(Stream stream)
        {
            _stream = stream;
            _buffer = new byte[1024];
        }

        /// <summary>
        /// Returns true if this source can be reset.
        /// </summary>
        public override bool CanReset
        {
            get
            {
                return _stream.CanSeek;
            }
        }

        /// <summary>
        /// Returns the current object.
        /// </summary>
        /// <returns></returns>
        public override OsmGeo Current()
        {
            return _current;
        }

        private OsmGeo _current;

        /// <summary>
        /// Move to the next object in this stream source.
        /// </summary>
        public override bool MoveNext(bool ignoreNodes, bool ignoreWays, bool ignoreRelations)
        {
            if (_stream.Length == _stream.Position + 1)
            {
                return false;
            }

            var osmGeo = this.DoMoveNext();
            while(osmGeo != null)
            {
                switch(osmGeo.Type)
                {
                    case OsmGeoType.Node:
                        if (!ignoreNodes)
                        {
                            _current = osmGeo;
                            return true;
                        }
                        break;
                    case OsmGeoType.Way:
                        if (!ignoreWays)
                        {
                            _current = osmGeo;
                            return true;
                        }
                        break;
                    case OsmGeoType.Relation:
                        if (!ignoreRelations)
                        {
                            _current = osmGeo;
                            return true;
                        }
                        break;
                }
                osmGeo = this.DoMoveNext();
            }
            return false;
        }

        private OsmGeo DoMoveNext()
        {
            return BinarySerializer.ReadOsmGeo(_stream, _buffer);
        }

        /// <summary>
        /// Resets this stream.
        /// </summary>
        public override void Reset()
        {
            _current = null;

            _stream.Seek(0, SeekOrigin.Begin);
        }
    }
}