using System.IO;

namespace OsmSharp.IO.Binary.Test.Functional
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // enable logging.
            OsmSharp.Logging.Logger.LogAction = (o, level, message, parameters) =>
            {
                System.Console.WriteLine(string.Format("[{0}] {1} - {2}", o, level, message));
            };

            Download.ToFile("http://files.itinero.tech/data/OSM/planet/europe/luxembourg-latest.osm.pbf", "test.osm.pbf").Wait();

            using (var sourceStream = File.OpenRead("test.osm.pbf"))
            using (var targetStream = File.OpenWrite("test.osm.bin"))
            {
                var source = new OsmSharp.Streams.PBFOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.IO.Binary.BinaryOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }

            using (var sourceStream = File.OpenRead("test.osm.bin"))
            using (var targetStream = File.OpenWrite("test2.osm.pbf"))
            {
                var source = new OsmSharp.IO.Binary.BinaryOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.Streams.PBFOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }
        }
    }
}
