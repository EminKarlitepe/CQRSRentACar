namespace CQRSRentACar.CQRSPattern.Queries.AirportQueries
{
    public class GetAirportByIdQuery
    {
        public int Id { get; set; }

        public GetAirportByIdQuery(int id)
        {
            Id = id;
        }
    }
}
