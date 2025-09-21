namespace CQRSRentACar.CQRSPattern.Commands.AboutCommand
{
    public class RemoveAboutCommand
    {
        public int Id { get; set; }

        public RemoveAboutCommand(int id)
        {
            Id = id;
        }
    }
}
