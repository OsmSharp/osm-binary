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

using OsmSharp.Tags;
using System;
using System.IO;

namespace OsmSharp.IO.Binary
{
    /// <summary>
    /// Contains all binary formatting code.
    /// </summary>
    public static class BinarySerializer
    {
        private static System.Text.Encoder _encoder = (new System.Text.UnicodeEncoding()).GetEncoder();
        
        /// <summary>
        /// Writes the given node starting at the stream's current position.
        /// </summary>
        public static int Append(this Stream stream, Node node)
        {
            if (node == null) { throw new ArgumentNullException(nameof(node)); }
            if (node.Id == null) { throw new ArgumentException("Object needs to have an id."); }
            if (node.ChangeSetId == null) { throw new ArgumentException("Object needs to have an changeset id."); }
            if (node.TimeStamp == null) { throw new ArgumentException("Object needs to have an timestamp."); }
            if (node.Version == null) { throw new ArgumentException("Object needs to have a version #."); }

            if (node.Latitude == null) { throw new ArgumentException("Object needs to have a latitude set."); }
            if (node.Longitude == null) { throw new ArgumentException("Object needs to have a longitude set."); }

            var size = 0;

            // write data.
            stream.WriteByte((byte)1); // a node.
            size += 1;
            size += stream.AppendOsmGeo(node);

            size += stream.Write(node.Latitude.Value);
            size += stream.Write(node.Longitude.Value);

            return size;
        }

        /// <summary>
        /// Writes the given way starting at the stream's current position.
        /// </summary>
        public static int Append(this Stream stream, Way way)
        {
            if (way == null) { throw new ArgumentNullException(nameof(way)); }
            if (way.Id == null) { throw new ArgumentException("Object needs to have an id."); }
            if (way.ChangeSetId == null) { throw new ArgumentException("Object needs to have an changeset id."); }
            if (way.TimeStamp == null) { throw new ArgumentException("Object needs to have an timestamp."); }
            if (way.Version == null) { throw new ArgumentException("Object needs to have a version #."); }

            var size = 0;

            // write data.
            stream.WriteByte((byte)2); // a way.
            size += 1;
            size += stream.AppendOsmGeo(way);
            
            if (way.Nodes == null ||
                way.Nodes.Length == 0)
            {
                size += stream.Write(0);
            }
            else
            {
                size += stream.Write(way.Nodes.Length);
                for (var i = 0; i < way.Nodes.Length; i++)
                {
                    size += stream.Write(way.Nodes[i]);
                }
            }

            return size;
        }

        /// <summary>
        /// Writes the given relation starting at the stream's current position.
        /// </summary>
        public static int Append(this Stream stream, Relation relation)
        {
            if (relation == null) { throw new ArgumentNullException(nameof(relation)); }
            if (relation.Id == null) { throw new ArgumentException("Object needs to have an id."); }
            if (relation.ChangeSetId == null) { throw new ArgumentException("Object needs to have an changeset id."); }
            if (relation.TimeStamp == null) { throw new ArgumentException("Object needs to have an timestamp."); }
            if (relation.Version == null) { throw new ArgumentException("Object needs to have a version #."); }

            var size = 0;

            // write data.
            stream.WriteByte((byte)3); // a relation.
            size += 1;
            size += stream.AppendOsmGeo(relation);
            
            if (relation.Members == null ||
                relation.Members.Length == 0)
            {
                size += stream.Write(0);
            }
            else
            {
                size += stream.Write(relation.Members.Length);
                for (var i = 0; i < relation.Members.Length; i++)
                {
                    size += stream.Write(relation.Members[i].Id);
                    size += stream.WriteWithSize(relation.Members[i].Role);
                    switch (relation.Members[i].Type)
                    {
                        case OsmGeoType.Node:
                            stream.WriteByte((byte)1);
                            break;
                        case OsmGeoType.Way:
                            stream.WriteByte((byte)2);
                            break;
                        case OsmGeoType.Relation:
                            stream.WriteByte((byte)3);
                            break;
                    }
                    size += 1;
                }
            }

            return size;
        }

        private static int AppendOsmGeo(this Stream stream, OsmGeo osmGeo)
        {
            var size = 0;

            size += stream.Write(osmGeo.Id.Value);
            size += stream.Write(osmGeo.ChangeSetId.Value);
            size += stream.Write(osmGeo.TimeStamp.Value.Ticks);
            size += stream.Write(osmGeo.UserId.Value);
            size += stream.WriteWithSize(osmGeo.UserName);
            size += stream.Write(osmGeo.Version.Value);
            size += stream.Write(osmGeo.Visible);

            if (osmGeo.Tags == null ||
                osmGeo.Tags.Count == 0)
            {
                stream.Write(0);
                size++;
            }
            else
            {
                stream.Write(osmGeo.Tags.Count);
                size++;
                foreach (var t in osmGeo.Tags)
                {
                    size += stream.WriteWithSize(t.Key);
                    size += stream.WriteWithSize(t.Value);
                }
            }

            return size;
        }
        
        /// <summary>
        /// Reads an OSM object starting at the stream's current position.
        /// </summary>
        public static OsmGeo ReadOsmGeo(this Stream stream, byte[] buffer)
        {
            var type = stream.ReadByte();
            if (type == -1)
            {
                return null;
            }
            switch(type)
            {
                case 1:
                    return stream.ReadNode(buffer);
                case 2:
                    return stream.ReadWay(buffer);
                case 3:
                    return stream.ReadRelation(buffer);
            }
            throw new Exception(string.Format("Invalid type: {0}.", type));
        }

        private static Node ReadNode(this Stream stream, byte[] buffer)
        {
            var node = new Node();

            stream.ReadOsmGeo(node, buffer);

            node.Latitude = stream.ReadSingle(buffer);
            node.Longitude = stream.ReadSingle(buffer);

            return node;
        }

        private static Way ReadWay(this Stream stream, byte[] buffer)
        {
            var way = new Way();

            stream.ReadOsmGeo(way, buffer);

            var nodeCount = stream.ReadInt32(buffer);
            if (nodeCount > 0)
            {
                var nodes = new long[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodes[i] = stream.ReadInt64(buffer);
                }
                way.Nodes = nodes;
            }

            return way;
        }

        private static Relation ReadRelation(this Stream stream, byte[] buffer)
        {
            var relation = new Relation();

            stream.ReadOsmGeo(relation, buffer);

            var memberCount = stream.ReadInt32(buffer);
            if (memberCount > 0)
            {
                var members = new RelationMember[memberCount];
                for(var i = 0; i< memberCount; i++)
                {
                    var id = stream.ReadInt64(buffer);
                    var role = stream.ReadWithSizeString(buffer);
                    var typeId = stream.ReadByte();
                    var type = OsmGeoType.Node;
                    switch(typeId)
                    {
                        case 2:
                            type = OsmGeoType.Way;
                            break;
                        case 3:
                            type = OsmGeoType.Relation;
                            break;
                    }
                    members[i] = new RelationMember()
                    {
                        Id = id,
                        Role = role,
                        Type = type
                    };
                }
                relation.Members = members;
            }

            return relation;
        }

        private static void ReadOsmGeo(this Stream stream, OsmGeo osmGeo, byte[] buffer)
        {
            osmGeo.Id = stream.ReadInt64(buffer);
            osmGeo.ChangeSetId = stream.ReadInt64(buffer);
            osmGeo.TimeStamp = stream.ReadDateTime(buffer);
            osmGeo.UserId = stream.ReadInt64(buffer);
            osmGeo.UserName = stream.ReadWithSizeString(buffer);
            osmGeo.Version = stream.ReadInt32(buffer);
            osmGeo.Visible = stream.ReadBoolNullable();

            var tagsSize = stream.ReadInt32(buffer);
            if (tagsSize > 0)
            {
                var tags = new TagsCollection();
                for (var t = 0; t < tagsSize; t++)
                {
                    var key = stream.ReadWithSizeString(buffer);
                    var value = stream.ReadWithSizeString(buffer);

                    tags.Add(key, value);
                }
                osmGeo.Tags = tags;
            }
        }

        /// <summary>
        /// Writes the given value to the stream.
        /// </summary>
        public static int Write(this Stream stream, int value)
        {
            stream.Write(BitConverter.GetBytes(value), 0, 4);
            return 4;
        }
        
        private static int Write(this Stream stream, float value)
        {
            stream.Write(BitConverter.GetBytes(value), 0, 4);
            return 4;
        }

        private static int Write(this Stream stream, long value)
        {
            stream.Write(BitConverter.GetBytes(value), 0, 8);
            return 8;
        }

        private static int Write(this Stream stream, ulong value)
        {
            stream.Write(BitConverter.GetBytes(value), 0, 8);
            return 8;
        }

        private static int Write(this Stream stream, DateTime value)
        {
            stream.Write(BitConverter.GetBytes(value.Ticks), 0, 8);
            return 8;
        }

        private static int Write(this Stream stream, bool? value)
        {
            if (value == null)
            {
                stream.WriteByte(0);
            }
            else if(value.Value)
            {
                stream.WriteByte(1);
            }
            else
            {
                stream.WriteByte(2);
            }
            return 4;
        }

        private static int WriteWithSize(this Stream stream, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                stream.WriteByte(0);
                return 1;
            }
            else
            { // TODO: improve this based on the protobuf way of handling this kind of variable info.
                var bytes = System.Text.Encoding.Unicode.GetBytes(value);
                var position = 0;
                while(bytes.Length - position >= 255)
                { // write in blocks of 255.
                    stream.WriteByte(255);
                    stream.Write(bytes, position, 255);
                    position += 256; // data + size
                }
                stream.WriteByte((byte)(bytes.Length - position));
                if (bytes.Length - position > 0)
                {
                    stream.Write(bytes, position, bytes.Length - position);
                }
                return bytes.Length + 1;
            }
        }

        private static DateTime ReadDateTime(this Stream stream, byte[] buffer)
        {
            return new DateTime(stream.ReadInt64(buffer));
        }

        private static long ReadInt64(this Stream stream, byte[] buffer)
        {
            stream.Read(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        private static int ReadInt32(this Stream stream, byte[] buffer)
        {
            stream.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        private static bool? ReadBoolNullable(this Stream stream)
        {
            var v = stream.ReadByte();
            if (v == 0)
            {
                return null;
            }
            else if (v == 1)
            {
                return true;
            }
            else if(v == 2)
            {
                return false;
            }
            else
            {
                throw new InvalidDataException("Cannot deserialize nullable bool.");
            }
        }

        private static float ReadSingle(this Stream stream, byte[] buffer)
        {
            stream.Read(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        private static string ReadWithSizeString(this System.IO.Stream stream, byte[] buffer)
        {
            var size = stream.ReadByte();
            var position = 0;
            while (size == 255)
            {
                stream.Read(buffer, position, (int)size);
                size = stream.ReadByte();
                position += 256;
            }
            if (size > 0)
            {
                stream.Read(buffer, position, (int)size);
            }


            return System.Text.UnicodeEncoding.Unicode.GetString(buffer, 0, size);
        }
    }
}