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

            // test reading/writing standard OSM-data.
            Download.ToFile("http://files.itinero.tech/data/OSM/planet/europe/luxembourg-latest.osm.pbf", "test.osm.pbf").Wait();

            using (var sourceStream = File.OpenRead("test.osm.pbf"))
            using (var targetStream = File.Open("test.osm.bin", FileMode.Create))
            {
                var source = new OsmSharp.Streams.PBFOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.Streams.BinaryOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }

            using (var sourceStream = File.OpenRead("test.osm.bin"))
            using (var targetStream = File.Open("test2.osm.pbf", FileMode.Create))
            {
                var source = new OsmSharp.Streams.BinaryOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.Streams.PBFOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }

            // test reading/writing edited OSM-data.
            using (var sourceStream = File.OpenRead("./test-data/data.osm"))
            using (var targetStream = File.Open("test.osm.bin", FileMode.Create))
            {
                var source = new OsmSharp.Streams.XmlOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.Streams.BinaryOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }

            using (var sourceStream = File.OpenRead("test.osm.bin"))
            using (var targetStream = File.Open("test2.osm.pbf", FileMode.Create))
            {
                var source = new OsmSharp.Streams.BinaryOsmStreamSource(sourceStream);
                var progress = new OsmSharp.Streams.Filters.OsmStreamFilterProgress();
                progress.RegisterSource(source);

                var target = new OsmSharp.Streams.PBFOsmStreamTarget(targetStream);
                target.RegisterSource(progress);
                target.Pull();
            }
        }
    }
}
