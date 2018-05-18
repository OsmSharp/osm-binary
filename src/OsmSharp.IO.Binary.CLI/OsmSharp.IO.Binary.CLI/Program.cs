using System;
using System.IO;

namespace OsmSharp.IO.Binary.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var inputStream = File.OpenRead(args[0]))
            using (var outputStream = File.Open(args[1], FileMode.Create))
            {
                var osmInputStream = new OsmSharp.Streams.BinaryOsmStreamSource(inputStream);
                var osmOutputStream = new OsmSharp.Streams.XmlOsmStreamTarget(outputStream);
                osmOutputStream.RegisterSource(osmInputStream);
                osmOutputStream.Pull();
                osmOutputStream.Flush();
            }
        }
    }
}