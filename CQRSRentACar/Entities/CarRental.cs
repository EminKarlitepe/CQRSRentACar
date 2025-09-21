namespace CQRSRentACar.Entities
{
    public class CarRental
    {
        public int CarRentalId { get; set; }
        public string? PickUpLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        // Airport ilişkileri
        public int? PickUpAirportId { get; set; }
        public Airport? PickUpAirport { get; set; }
        
        public int? DropOffAirportId { get; set; }
        public Airport? DropOffAirport { get; set; }
    }
}
