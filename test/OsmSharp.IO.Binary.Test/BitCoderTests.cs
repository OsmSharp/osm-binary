using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OsmSharp.IO.Binary.Test
{
    [TestClass]
    public class BitCoderTests
    {
        [TestMethod]
        public void BitCoder_WriteInt32_ShouldWrite4BytesAndValue()
        {
            var data = new MemoryStream();

            var test = 1457167146;
            BitCoder.WriteInt32(data, test);

            Assert.AreEqual(4, data.Length);
            data.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[4];
            data.Read(buffer, 0, 4);
            var result = BitConverter.ToInt32(buffer);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_ReadInt32_ShouldReadValue()
        {
            var data = new MemoryStream();

            var test = 1457167146;
            data.Write(BitConverter.GetBytes(test));

            data.Seek(0, SeekOrigin.Begin);
            var result = data.ReadInt32();

            Assert.AreEqual(4, data.Length);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_WriteInt64_ShouldWrite8BytesAndValue()
        {
            var data = new MemoryStream();

            var test = 1457167458121344146L;
            data.WriteInt64(test);

            Assert.AreEqual(8, data.Length);
            data.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[8];
            data.Read(buffer, 0, 8);
            var result = BitConverter.ToInt64(buffer);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_ReadInt64_ShouldReadValue()
        {
            var data = new MemoryStream();

            var test = 1457167458121344146L;
            data.Write(BitConverter.GetBytes(test));

            data.Seek(0, SeekOrigin.Begin);
            var result = data.ReadInt64();

            Assert.AreEqual(8, data.Length);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_WriteUInt32_ShouldWrite4BytesAndValue()
        {
            var data = new MemoryStream();

            var test = 1457167146U;
            data.WriteUInt32(test);

            Assert.AreEqual(4, data.Length);
            data.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[8];
            data.Read(buffer, 0, 8);
            var result = BitConverter.ToUInt32(buffer);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_ReadUInt32_ShouldReadValue()
        {
            var data = new MemoryStream();

            var test = 1457167412U;
            data.Write(BitConverter.GetBytes(test));

            data.Seek(0, SeekOrigin.Begin);
            var result = data.ReadUInt32();

            Assert.AreEqual(4,data.Length);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_WriteUInt64_ShouldWrite8BytesAndValue()
        {
            var data = new MemoryStream();

            var test = 1457167458121344146UL;
            data.WriteUInt64(test);

            Assert.AreEqual(8, data.Length);
            data.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[8];
            data.Read(buffer, 0, 8);
            var result = BitConverter.ToUInt64(buffer);
            Assert.AreEqual(test, result);
        }
        
        [TestMethod]
        public void BitCoder_ReadUInt64_ShouldReadValue()
        {
            var data = new MemoryStream();

            var test = 1457167458121344146UL;
            data.Write(BitConverter.GetBytes(test));

            data.Seek(0, SeekOrigin.Begin);
            var result = data.ReadUInt64();

            Assert.AreEqual(8, data.Length);
            Assert.AreEqual(test, result);
        }
    }
}