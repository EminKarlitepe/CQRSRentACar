namespace CQRSRentACar.CQRSPattern.Commands.FeatureCommands
{
    public class RemoveFeatureCommand
    {
        public int Id { get; set; }

        public RemoveFeatureCommand(int id)
        {
            Id = id;
        }
    }
}
