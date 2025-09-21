namespace CQRSRentACar.CQRSPattern.Commands.CarRentalCommands
{
    public class UpdateCarRentalCommand
    {
        public int CarRentalId { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }

        public int CarId { get; set; }
    }
}
