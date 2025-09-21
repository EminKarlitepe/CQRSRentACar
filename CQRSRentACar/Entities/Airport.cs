namespace CQRSRentACar.Entities
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string? Iata { get; set; }
        public string? Icao { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? CountryIso { get; set; }
        public string? Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
