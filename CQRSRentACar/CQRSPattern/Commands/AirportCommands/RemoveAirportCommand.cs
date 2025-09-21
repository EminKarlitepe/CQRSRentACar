namespace CQRSRentACar.CQRSPattern.Commands.AirportCommands
{
    public class RemoveAirportCommand
    {
        public int AirportId { get; set; }

        public RemoveAirportCommand(int airportId)
        {
            AirportId = airportId;
        }
    }
}
