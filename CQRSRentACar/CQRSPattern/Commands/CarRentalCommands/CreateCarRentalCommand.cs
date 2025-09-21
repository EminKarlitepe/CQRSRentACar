namespace CQRSRentACar.CQRSPattern.Commands.CarRentalCommands
{
    public class CreateCarRentalCommand
    {
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public int CarId { get; set; }
        public int? PickUpAirportId { get; set; }
        public int? DropOffAirportId { get; set; }
    }
}
