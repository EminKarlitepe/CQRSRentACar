namespace CQRSRentACar.CQRSPattern.Commands.ServiceCommands
{
    public class UpdateServiceCommand
    {
        public int ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceIcon { get; set; }
    }
}
