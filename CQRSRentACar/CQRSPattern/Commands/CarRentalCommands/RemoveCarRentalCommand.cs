namespace CQRSRentACar.CQRSPattern.Commands.CarRentalCommands
{
    public class RemoveCarRentalCommand
    {
        public int Id { get; set; }

        public RemoveCarRentalCommand(int id)
        {
            Id = id;
        }
    }
}
