# OsmSharp.IO.Binary

An IO module on top of OsmSharp that reads/writes OSM data in a **custom binary format**.

[![Build status](https://build.anyways.eu/app/rest/builds/buildType:(id:anyways_Core_RoutingProfiles)/statusIcon)](https://build.anyways.eu/viewType.html?buildTypeId=anyways_Core_RoutingProfiles)  

We built this because it more efficient compared to OSM-XML and has some advantags over OSM-PBF. This can be used to:

- Read/write indivdual objects.
- Read/write objects with negative ids
- Read/write objects missing data (even missing ids, versions, etc.).
- Can be streamed.
- Can be partially read when positions are known.
