namespace CQRSRentACar.CQRSPattern.Results.CarResults
{
    public class GetCarByIdQueryResult
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarImageUrl { get; set; }
        public int Rating { get; set; }
        public int Price { get; set; }
        public int Seat { get; set; }
        public string Transmission { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; }
        public int ModelYear { get; set; }
        public string Gear { get; set; }
        public int Kilometer { get; set; }
    }
}
