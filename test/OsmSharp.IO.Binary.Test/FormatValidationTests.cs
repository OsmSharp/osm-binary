using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsmSharp.Streams;
using OsmSharp.Tags;

namespace OsmSharp.IO.Binary.Test
{
    /// <summary>
    /// A set of tests to prevent regression in the binary format.
    /// </summary>
    [TestClass]
    public class FormatValidationTests
    {
        [TestMethod]
        public void ReadNode_StreamPreviousVersion_ShouldReadNode()
        {
            using (var stream = Staging.TestStreams.GetNodeRegressionStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var nodes = sourceStream.ToList();
                
                Assert.AreEqual(1, nodes.Count);
                var osmGeo = nodes[0];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Node));
                var node = osmGeo as Node;
                Assert.IsNotNull(node);
                Assert.AreEqual(1, node.Id);
                Assert.AreEqual(2, node.ChangeSetId);
                Assert.AreEqual(10, node.Latitude);
                Assert.AreEqual(11, node.Longitude);
                Assert.IsNotNull(node.TimeStamp);
                Assert.AreEqual(637209093198843556,node.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Node, node.Type);
                Assert.AreEqual(12, node.UserId);
                Assert.AreEqual("Ben", node.UserName);
                Assert.AreEqual(123, node.Version);
                Assert.IsNotNull(node.Visible);
                Assert.IsTrue(node.Visible.Value);
                
                Assert.IsNotNull(node.Tags);
                Assert.AreEqual(1, node.Tags.Count);
                Assert.IsTrue(node.Tags.Contains("name", "hu?"));
            }
        }
        
        [TestMethod]
        public void ReadWay_StreamPreviousVersion_ShouldReadWay()
        {
            using (var stream = Staging.TestStreams.GetWayRegressionStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var ways = sourceStream.ToList();
                
                Assert.AreEqual(1, ways.Count);
                var osmGeo = ways[0];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Way));
                var way = osmGeo as Way;
                Assert.IsNotNull(way);
                Assert.AreEqual(1, way.Id);
                Assert.AreEqual(1, way.ChangeSetId);
                Assert.IsNotNull(way.TimeStamp);
                Assert.AreEqual(637209093234745239,way.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Way, way.Type);
                Assert.AreEqual(1, way.UserId);
                Assert.AreEqual("Ben", way.UserName);
                Assert.AreEqual(1, way.Version);
                Assert.IsNotNull(way.Visible);
                Assert.IsTrue(way.Visible.Value);
                
                Assert.IsNotNull(way.Tags);
                Assert.AreEqual(1, way.Tags.Count);
                Assert.IsTrue(way.Tags.Contains("name", "hu?"));
                
                Assert.IsNotNull(way.Nodes);
                Assert.AreEqual(3, way.Nodes.Length);
                Assert.AreEqual(1, way.Nodes[0]);
                Assert.AreEqual(2, way.Nodes[1]);
                Assert.AreEqual(3, way.Nodes[2]);
            }
        }
        
        [TestMethod]
        public void ReadRelation_StreamPreviousVersion_ShouldReadRelation()
        {
            using (var stream = Staging.TestStreams.GetRelationRegressionStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var relations = sourceStream.ToList();
                
                Assert.AreEqual(1, relations.Count);
                var osmGeo = relations[0];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Relation));
                var relation = osmGeo as Relation;
                Assert.IsNotNull(relation);
                Assert.AreEqual(1, relation.Id);
                Assert.AreEqual(1, relation.ChangeSetId);
                Assert.IsNotNull(relation.TimeStamp);
                Assert.AreEqual(637209093261203018,relation.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Relation, relation.Type);
                Assert.AreEqual(1, relation.UserId);
                Assert.AreEqual("Ben", relation.UserName);
                Assert.AreEqual(1, relation.Version);
                Assert.IsNotNull(relation.Visible);
                Assert.IsTrue(relation.Visible.Value);
                
                Assert.IsNotNull(relation.Tags);
                Assert.AreEqual(1, relation.Tags.Count);
                Assert.IsTrue(relation.Tags.Contains("name", "hu?"));
                
                Assert.IsNotNull(relation.Members);
                Assert.AreEqual(3, relation.Members.Length);
                Assert.AreEqual(1, relation.Members[0].Id);
                Assert.AreEqual(OsmGeoType.Node, relation.Members[0].Type);
                Assert.AreEqual("node", relation.Members[0].Role);
                Assert.AreEqual(2, relation.Members[1].Id);
                Assert.AreEqual(OsmGeoType.Way, relation.Members[1].Type);
                Assert.AreEqual("way", relation.Members[1].Role);
                Assert.AreEqual(3, relation.Members[2].Id);
                Assert.AreEqual(OsmGeoType.Relation, relation.Members[2].Type);
                Assert.AreEqual("relation", relation.Members[2].Role);
            }
        }
        
        [TestMethod]
        public void ReadAll_StreamPreviousVersion_ShouldReadAll()
        {
            using (var stream = Staging.TestStreams.GetAllRegressionStream())
            {
                var sourceStream = new BinaryOsmStreamSource(stream);
                var osmGeos = sourceStream.ToList();
                
                Assert.AreEqual(3, osmGeos.Count);
                
                var osmGeo = osmGeos[0];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Node));
                var node = osmGeo as Node;
                Assert.IsNotNull(node);
                Assert.AreEqual(1, node.Id);
                Assert.AreEqual(2, node.ChangeSetId);
                Assert.AreEqual(10, node.Latitude);
                Assert.AreEqual(11, node.Longitude);
                Assert.IsNotNull(node.TimeStamp);
                Assert.AreEqual(637209111424214668,node.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Node, node.Type);
                Assert.AreEqual(12, node.UserId);
                Assert.AreEqual("Ben", node.UserName);
                Assert.AreEqual(123, node.Version);
                Assert.IsNotNull(node.Visible);
                Assert.IsTrue(node.Visible.Value);
                
                Assert.IsNotNull(node.Tags);
                Assert.AreEqual(1, node.Tags.Count);
                Assert.IsTrue(node.Tags.Contains("name", "hu?"));
                
                osmGeo = osmGeos[1];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Way));
                var way = osmGeo as Way;
                Assert.IsNotNull(way);
                Assert.AreEqual(1, way.Id);
                Assert.AreEqual(1, way.ChangeSetId);
                Assert.IsNotNull(way.TimeStamp);
                Assert.AreEqual(637209111424421422,way.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Way, way.Type);
                Assert.AreEqual(1, way.UserId);
                Assert.AreEqual("Ben", way.UserName);
                Assert.AreEqual(1, way.Version);
                Assert.IsNotNull(way.Visible);
                Assert.IsTrue(way.Visible.Value);
                
                Assert.IsNotNull(way.Tags);
                Assert.AreEqual(1, way.Tags.Count);
                Assert.IsTrue(way.Tags.Contains("name", "hu?"));
                
                Assert.IsNotNull(way.Nodes);
                Assert.AreEqual(3, way.Nodes.Length);
                Assert.AreEqual(1, way.Nodes[0]);
                Assert.AreEqual(2, way.Nodes[1]);
                Assert.AreEqual(3, way.Nodes[2]);
                
                osmGeo = osmGeos[2];
                Assert.IsNotNull(osmGeo);
                Assert.IsInstanceOfType(osmGeo, typeof(Relation));
                var relation = osmGeo as Relation;
                Assert.IsNotNull(relation);
                Assert.AreEqual(1, relation.Id);
                Assert.AreEqual(1, relation.ChangeSetId);
                Assert.IsNotNull(relation.TimeStamp);
                Assert.AreEqual(637209111424429133,relation.TimeStamp.Value.Ticks);
                Assert.AreEqual(OsmGeoType.Relation, relation.Type);
                Assert.AreEqual(1, relation.UserId);
                Assert.AreEqual("Ben", relation.UserName);
                Assert.AreEqual(1, relation.Version);
                Assert.IsNotNull(relation.Visible);
                Assert.IsTrue(relation.Visible.Value);
                
                Assert.IsNotNull(relation.Tags);
                Assert.AreEqual(1, relation.Tags.Count);
                Assert.IsTrue(relation.Tags.Contains("name", "hu?"));
                
                Assert.IsNotNull(relation.Members);
                Assert.AreEqual(3, relation.Members.Length);
                Assert.AreEqual(1, relation.Members[0].Id);
                Assert.AreEqual(OsmGeoType.Node, relation.Members[0].Type);
                Assert.AreEqual("node", relation.Members[0].Role);
                Assert.AreEqual(2, relation.Members[1].Id);
                Assert.AreEqual(OsmGeoType.Way, relation.Members[1].Type);
                Assert.AreEqual("way", relation.Members[1].Role);
                Assert.AreEqual(3, relation.Members[2].Id);
                Assert.AreEqual(OsmGeoType.Relation, relation.Members[2].Type);
                Assert.AreEqual("relation", relation.Members[2].Role);
            }
        }

        [TestMethod]
        public void WriteNode_ShouldMatch_StreamPreviousVersion()
        {
            using (var stream = new MemoryStream())
            {
                var targetStream = new BinaryOsmStreamTarget(stream);
                targetStream.Initialize();
                targetStream.AddNode(new Node()
                {
                    Id = 1,
                    ChangeSetId = 2,
                    Latitude = 10,
                    TimeStamp = new DateTime(637209093198843556),
                    Longitude = 11,
                    Tags = new TagsCollection(new Tag("name", "hu?")),
                    UserId = 12,
                    UserName = "Ben",
                    Version = 123,
                    Visible = true
                });

                stream.Seek(0, SeekOrigin.Begin);
                var bytes = stream.ToArray();
                var regressionBytes = new byte[bytes.Length];

                using (var regressionStream = Staging.TestStreams.GetNodeRegressionStream())
                {
                    Assert.AreEqual(bytes.Length, regressionStream.Length);
                    regressionStream.Read(regressionBytes, 0, regressionBytes.Length);
                }

                for (var b = 0; b < bytes.Length; b++)
                {
                    Assert.AreEqual(regressionBytes[b], bytes[b]);
                }
            }
        }

        [TestMethod]
        public void WriteWay_ShouldMatch_StreamPreviousVersion()
        {
            using (var stream = new MemoryStream())
            {
                var targetStream = new BinaryOsmStreamTarget(stream);
                targetStream.Initialize();
                targetStream.AddWay(new Way()
                {
                    Id = 1,
                    ChangeSetId = 1,
                    TimeStamp = new DateTime(637209093234745239),
                    Tags = new TagsCollection(new Tag("name", "hu?")),
                    Nodes = new long[]
                    {
                        1,
                        2, 
                        3
                    },
                    UserId = 1,
                    UserName = "Ben",
                    Version = 1,
                    Visible = true
                });

                stream.Seek(0, SeekOrigin.Begin);
                var bytes = stream.ToArray();
                var regressionBytes = new byte[bytes.Length];

                using (var regressionStream = Staging.TestStreams.GetWayRegressionStream())
                {
                    Assert.AreEqual(bytes.Length, regressionStream.Length);
                    regressionStream.Read(regressionBytes, 0, regressionBytes.Length);
                }

                for (var b = 0; b < bytes.Length; b++)
                {
                    Assert.AreEqual(regressionBytes[b], bytes[b]);
                }
            }
        }

        [TestMethod]
        public void WriteRelation_ShouldMatch_StreamPreviousVersion()
        {
            using (var stream = new MemoryStream())
            {
                var targetStream = new BinaryOsmStreamTarget(stream);
                targetStream.Initialize();
                targetStream.AddRelation(new Relation()
                {
                    Id = 1,
                    ChangeSetId = 1,
                    TimeStamp = new DateTime(637209093261203018),
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

                stream.Seek(0, SeekOrigin.Begin);
                var bytes = stream.ToArray();
                var regressionBytes = new byte[bytes.Length];

                using (var regressionStream = Staging.TestStreams.GetRelationRegressionStream())
                {
                    Assert.AreEqual(bytes.Length, regressionStream.Length);
                    regressionStream.Read(regressionBytes, 0, regressionBytes.Length);
                }

                for (var b = 0; b < bytes.Length; b++)
                {
                    Assert.AreEqual(regressionBytes[b], bytes[b]);
                }
            }
        }

        [TestMethod]
        public void WriteAll_ShouldMatch_StreamPreviousVersion()
        {
            using (var stream = new MemoryStream())
            {
                var targetStream = new BinaryOsmStreamTarget(stream);
                targetStream.Initialize();
                targetStream.AddNode(new Node()
                {
                    Id = 1,
                    ChangeSetId = 2,
                    Latitude = 10,
                    TimeStamp = new DateTime(637209111424214668),
                    Longitude = 11,
                    Tags = new TagsCollection(new Tag("name", "hu?")),
                    UserId = 12,
                    UserName = "Ben",
                    Version = 123,
                    Visible = true
                });
                targetStream.AddWay(new Way()
                {
                    Id = 1,
                    ChangeSetId = 1,
                    TimeStamp = new DateTime(637209111424421422),
                    Tags = new TagsCollection(new Tag("name", "hu?")),
                    Nodes = new long[]
                    {
                        1,
                        2, 
                        3
                    },
                    UserId = 1,
                    UserName = "Ben",
                    Version = 1,
                    Visible = true
                });
                targetStream.AddRelation(new Relation()
                {
                    Id = 1,
                    ChangeSetId = 1,
                    TimeStamp = new DateTime(637209111424429133),
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

                stream.Seek(0, SeekOrigin.Begin);
                var bytes = stream.ToArray();
                var regressionBytes = new byte[bytes.Length];

                using (var regressionStream = Staging.TestStreams.GetAllRegressionStream())
                {
                    Assert.AreEqual(bytes.Length, regressionStream.Length);
                    regressionStream.Read(regressionBytes, 0, regressionBytes.Length);
                }

                for (var b = 0; b < bytes.Length; b++)
                {
                    Assert.AreEqual(regressionBytes[b], bytes[b]);
                }
            }
        }
    }
}