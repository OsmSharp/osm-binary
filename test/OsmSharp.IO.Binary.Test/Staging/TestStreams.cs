using System;
using System.IO;
using OsmSharp.Tags;

namespace OsmSharp.IO.Binary.Test.Staging
{
    public static class TestStreams
    {
        /// <summary>
        /// Creates a test stream with a single node.
        /// </summary>
        /// <returns></returns>
        public static Stream GetNodeTestStream()
        {
            var stream = new MemoryStream();
            var node1 = new Node()
            {
                Id = 1,
                ChangeSetId = 2,
                Latitude = 10,
                TimeStamp = DateTime.Now,
                Longitude = 11,
                Tags = new TagsCollection(new Tag("name", "hu?")),
                UserId = 12,
                UserName = "Ben",
                Version = 123,
                Visible = true
            };

            stream.Append(node1);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        
        /// <summary>
        /// Creates a test stream with a single way.
        /// </summary>
        /// <returns></returns>
        public static Stream GetWayTestStream()
        {
            var stream = new MemoryStream();
            var way1 = new Way()
            {
                Id = 1,
                ChangeSetId = 1,
                TimeStamp = DateTime.Now,
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
            };

            stream.Append(way1);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        
        /// <summary>
        /// Creates a test stream with a single relation.
        /// </summary>
        /// <returns></returns>
        public static Stream GetRelationTestStream()
        {
            var stream = new MemoryStream();
            var relation1 = new Relation()
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
            };

            stream.Append(relation1);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        
        /// <summary>
        /// Creates a test stream with a single node, way and relation.
        /// </summary>
        /// <returns></returns>
        public static Stream GetNodeWayAndRelationTestStream()
        {
            var stream = new MemoryStream();
            var node1 = new Node()
            {
                Id = 1,
                ChangeSetId = 2,
                Latitude = 10,
                TimeStamp = DateTime.Now,
                Longitude = 11,
                Tags = new TagsCollection(new Tag("name", "hu?")),
                UserId = 12,
                UserName = "Ben",
                Version = 123,
                Visible = true
            };

            stream.Append(node1);
            
            var way1 = new Way()
            {
                Id = 1,
                ChangeSetId = 1,
                TimeStamp = DateTime.Now,
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
            };

            stream.Append(way1);
            
            var relation1 = new Relation()
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
            };

            stream.Append(relation1);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}