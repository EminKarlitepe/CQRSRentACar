namespace CQRSRentACar.CQRSPattern.Commands.TestimonialCommands
{
    public class UpdateTestimonialCommand
    {
        public int TestimonialId { get; set; }
        public string TestimonialNameSurname { get; set; }
        public string TestimonialPosition { get; set; }
        public string TestimonialRating { get; set; }
        public string TestimonialComment { get; set; }
        public string TestimonialImageUrl { get; set; }
    }
}
