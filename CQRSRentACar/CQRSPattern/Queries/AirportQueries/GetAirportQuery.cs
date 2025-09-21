namespace CQRSRentACar.CQRSPattern.Queries.AirportQueries
{
    public class GetAirportQuery
    {
        public string? CountryCode { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
}
