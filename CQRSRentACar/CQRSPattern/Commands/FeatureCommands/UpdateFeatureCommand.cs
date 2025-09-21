namespace CQRSRentACar.CQRSPattern.Commands.FeatureCommands
{
    public class UpdateFeatureCommand
    {
        public int FeatureId { get; set; }
        public string FeatureTitle { get; set; }
        public string FeatureDescription { get; set; }
        public string FeatureIcon { get; set; }
    }
}
