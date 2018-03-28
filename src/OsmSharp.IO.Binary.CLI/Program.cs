using System;
using System.IO;
using OsmSharp.Streams;

namespace OsmSharp.IO.Binary.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sourceStream = File.OpenRead(args[0]))
            using (var targetStream = File.Open(args[1], FileMode.Create))
            {
                var sourceBinary = new BinaryOsmStreamSource(sourceStream);
                var targetXml = new XmlOsmStreamTarget(targetStream);
                targetXml.RegisterSource(sourceBinary);
                targetXml.Pull();
            }
        }
    }
}
