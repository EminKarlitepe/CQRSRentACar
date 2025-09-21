namespace CQRSRentACar.CQRSPattern.Commands.AboutCommand
{
    public class UpdateAboutCommand
    {
        public int AboutId { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string VisionDescription { get; set; }
        public string MisionDescription { get; set; }
    }
}
