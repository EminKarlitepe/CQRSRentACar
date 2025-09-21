namespace CQRSRentACar.CQRSPattern.Commands.SliderCommands
{
    public class UpdateSliderCommand
    {
        public int SliderId { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubTitle { get; set; }
        public string SliderImageUrl { get; set; }
    }
}
