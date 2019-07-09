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

using OsmSharp.IO.Binary;
using System.IO;

namespace OsmSharp.Streams
{
    /// <summary>
    /// A stream target that just writes objects in binary format.
    /// </summary>
    public class BinaryOsmStreamTarget : OsmSharp.Streams.OsmStreamTarget
    {
        private readonly Stream _stream;

        /// <summary>
        /// Creates a new stream target.
        /// </summary>
        public BinaryOsmStreamTarget(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Adds a node.
        /// </summary>
        /// <param name="node"></param>
        public override void AddNode(Node node)
        {
            _stream.Append(node);
        }

        /// <summary>
        /// Adds a relation.
        /// </summary>
        /// <param name="relation"></param>
        public override void AddRelation(Relation relation)
        {
            _stream.Append(relation);
        }

        /// <summary>
        /// Adds a way.
        /// </summary>
        /// <param name="way"></param>
        public override void AddWay(Way way)
        {
            _stream.Append(way);
        }
        
        /// <summary>
        /// Flushes data in this stream.
        /// </summary>
        public override void Flush()
        {
            _stream.Flush();
        }

        /// <summary>
        /// Closes this target.
        /// </summary>
        public override void Close()
        {
            this.Flush();
        }

        /// <summary>
        /// Initializes this target.
        /// </summary>
        public override void Initialize()
        { 

        }
    }
}