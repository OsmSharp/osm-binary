using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace OsmSharp.IO.Binary.Test
{
    /// <summary>
    /// Contains tests for the binary serializer core functions.
    /// </summary>
    [TestClass]
    public class BinarySerializerTests
    {
        /// <summary>
        /// Tests reading/writing a node.
        /// </summary>
        [TestMethod]
        public void TestReadWriteNode()
        {
            using (var stream = new MemoryStream())
            {
                var node1 = new Node()
                {
                    Id = 10,
                    Latitude = 100,
                    Longitude = 101
                };

                BinarySerializer.Append(stream, node1);

                stream.Seek(0, SeekOrigin.Begin);

                var osmGeo = BinarySerializer.ReadOsmGeo(stream, new byte[1024]);
                Assert.IsInstanceOfType(osmGeo, typeof(Node));
                var node2 = osmGeo as Node;
                
            }
        }
    }
}
