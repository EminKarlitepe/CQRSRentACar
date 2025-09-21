namespace CQRSRentACar.Models
{
    public class AirportDistanceViewModel
    {
        public string? Status { get; set; }
        public string? Error { get; set; }
        public DistanceData? Data { get; set; }

        public class DistanceData
        {
            public double DistanceMiles { get; set; }
            public double DistanceKm { get; set; }
            public AirportInfo? Airport1 { get; set; }
            public AirportInfo? Airport2 { get; set; }
        }

        public class AirportInfo
        {
            public string? Name { get; set; }
            public string? Iata { get; set; }
            public string? Icao { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Country { get; set; }
            public double Elevation { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
