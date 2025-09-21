namespace CQRSRentACar.CQRSPattern.Queries.CarRentalQueries
{
    public class GetCarRentalByIdQuery
    {
        public int Id { get; set; }

        public GetCarRentalByIdQuery(int id)
        {
            Id = id;
        }
    }
}
