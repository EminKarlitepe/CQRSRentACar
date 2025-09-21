namespace CQRSRentACar.CQRSPattern.Queries.CarRentalQueries
{
    public class GetRentedCarIdsQuery
    {
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string? PickUpLocation { get; set; }
        public string? DropOffLocation { get; set; }

        public GetRentedCarIdsQuery(DateTime pickUpDate, DateTime dropOffDate, string? pickUpLocation = null, string? dropOffLocation = null)
        {
            PickUpDate = pickUpDate;
            DropOffDate = dropOffDate;
            PickUpLocation = pickUpLocation;
            DropOffLocation = dropOffLocation;
        }
    }
}
