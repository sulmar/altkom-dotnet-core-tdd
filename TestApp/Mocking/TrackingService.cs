using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGeoHash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    // dotnet add package NGeoHash

    /*
    public class TrackingService
    {
        public Location Get()
        {
            string json = File.ReadAllText("tracking.txt");

            Location location = JsonConvert.DeserializeObject<Location>(json);

            if (location == null)
                throw new ApplicationException("Error parsing the location");

            return location;
        }


        // geohash.org
        public string GetPathAsGeoHash()
        {
            IList<string> path = new List<string>();

            using (var context = new TrackingContext())
            {
                var locations = context.Trackings.Where(t=>t.ValidGPS).Select(t=>t.Location).ToList();

                foreach (Location location in locations)
                {
                    path.Add(GeoHash.Encode(location.Latitude, location.Longitude));
                }

                return string.Join(",", path);
                    
            }
        }
    }

    */

    public interface ITrackingService
    {
        Location Get();
    }

   
    public interface IFileReader
    {
        string ReadAllText(string path);
    }

    public interface IFileReaderAsync
    {
        Task<string> ReadAllTextAsync(string path);
    }

    public class FileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }

    public interface ITrackingServiceAsync
    {
        Task<Location> GetAsync();
    }

    public class TrackingServiceAsync : ITrackingServiceAsync
    {
        private readonly IFileReaderAsync fileReader;

        public TrackingServiceAsync(IFileReaderAsync fileReader)
        {
            this.fileReader = fileReader;
        }

        public async Task<Location> GetAsync()
        {
            string json = await fileReader.ReadAllTextAsync("tracking.json");

            try
            {
                Location location = JsonConvert.DeserializeObject<Location>(json);

                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;
            }
            catch (JsonReaderException)
            {
                throw new FormatException();
            }
        }

      

    }



        public class TrackingService : ITrackingService
    {
        private readonly IFileReader fileReader;

        public TrackingService(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public Location Get()
        {
            string json = fileReader.ReadAllText("tracking.json");

            try
            {
                Location location = JsonConvert.DeserializeObject<Location>(json);

                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;
            }
            catch(JsonReaderException)
            {
                throw new FormatException();
            }
        }

        public Task<Location> GetAsync()
        {
            return Task.FromResult(Get());
        }


        // geohash.org
        public string GetPathAsGeoHash()
        {
            IList<string> path = new List<string>();

            using (var context = new TrackingContext())
            {
                var locations = context.Trackings.Where(t => t.ValidGPS).Select(t => t.Location).ToList();

                foreach (Location location in locations)
                {
                    path.Add(GeoHash.Encode(location.Latitude, location.Longitude));
                }

                return string.Join(",", path);

            }
        }
    }


    public class TrackingContext : DbContext
    {
        public DbSet<Tracking> Trackings { get; set; }
    }

    public class Location
    {
        public Location()
        {

        }
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }

    }

    public class Tracking
    {
        public Location Location { get; set; }
        public byte Satellites { get; set; }
        public bool ValidGPS { get; set; }
    }


}
