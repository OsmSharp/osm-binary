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

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsmSharp.Streams;
using OsmSharp.Tags;

namespace OsmSharp.IO.Binary.Test
{
    /// <summary>
    /// Contains tests for the binary serializer core functions.
    /// </summary>
    [TestClass]
    public class BinaryOsmStreamSourceTests
    {
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldReadNode()
        {
            using (var stream = Staging.TestStreams.GetNodeTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var nodes = sourceStream.ToList();
                
                Assert.AreEqual(1, nodes.Count);
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldReadWay()
        {
            using (var stream = Staging.TestStreams.GetWayTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var ways = sourceStream.ToList();
                
                Assert.AreEqual(1, ways.Count);
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldReadRelation()
        {
            using (var stream = Staging.TestStreams.GetRelationTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var relations = sourceStream.ToList();
                
                Assert.AreEqual(1, relations.Count);
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldReadNodeWayAndRelation()
        {
            using (var stream = Staging.TestStreams.GetNodeWayAndRelationTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var osmGeos = sourceStream.ToList();
                
                Assert.AreEqual(3, osmGeos.Count);
                Assert.IsInstanceOfType(osmGeos[0], typeof(Node));
                Assert.IsInstanceOfType(osmGeos[1], typeof(Way));
                Assert.IsInstanceOfType(osmGeos[2], typeof(Relation));
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldIgnoreNode()
        {
            using (var stream = Staging.TestStreams.GetNodeWayAndRelationTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var osmGeos = sourceStream.ToList();
                
                sourceStream.Reset();
                osmGeos = sourceStream.EnumerateAndIgore(true, false, false).ToList();
                
                Assert.AreEqual(2, osmGeos.Count);
                Assert.IsInstanceOfType(osmGeos[0], typeof(Way));
                Assert.IsInstanceOfType(osmGeos[1], typeof(Relation));
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldIgnoreWay()
        {
            using (var stream = Staging.TestStreams.GetNodeWayAndRelationTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var osmGeos = sourceStream.ToList();
                
                sourceStream.Reset();
                osmGeos = sourceStream.EnumerateAndIgore(false, true, false).ToList();
                
                Assert.AreEqual(2, osmGeos.Count);
                Assert.IsInstanceOfType(osmGeos[0], typeof(Node));
                Assert.IsInstanceOfType(osmGeos[1], typeof(Relation));
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldIgnoreRelation()
        {
            using (var stream = Staging.TestStreams.GetNodeWayAndRelationTestStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var osmGeos = sourceStream.ToList();
                
                sourceStream.Reset();
                osmGeos = sourceStream.EnumerateAndIgore(false, false, true).ToList();
                
                Assert.AreEqual(2, osmGeos.Count);
                Assert.IsInstanceOfType(osmGeos[0], typeof(Node));
                Assert.IsInstanceOfType(osmGeos[1], typeof(Way));
            }
        }
        
        [TestMethod]
        public void BinaryOsmStreamSource_ShouldReadFomDeflateStream()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamCompressed = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    var targetStream = new BinaryOsmStreamTarget(streamCompressed);
                    targetStream.Initialize();
                    targetStream.AddRelation(new Relation()
                    {
                        Id = 1,
                        ChangeSetId = 1,
                        TimeStamp = DateTime.Now,
                        Tags = new TagsCollection(new Tag("name", "hu?")),
                        Members = new RelationMember[]
                        {
                            new RelationMember()
                            {
                                Id = 1,
                                Role = "node",
                                Type = OsmGeoType.Node
                            },
                            new RelationMember()
                            {
                                Id = 2,
                                Role = "way",
                                Type = OsmGeoType.Way
                            },
                            new RelationMember()
                            {
                                Id = 3,
                                Role = "relation",
                                Type = OsmGeoType.Relation
                            }
                        },
                        UserId = 1,
                        UserName = "Ben",
                        Version = 1,
                        Visible = true
                    });
                }

                stream.Seek(0, SeekOrigin.Begin);
                using (var streamCompressed = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    var sourceStream = new BinaryOsmStreamSource(streamCompressed);
                    var osmGeos = sourceStream.ToList();
                
                    Assert.AreEqual(1, osmGeos.Count);
                }
            }
        }
    }
}