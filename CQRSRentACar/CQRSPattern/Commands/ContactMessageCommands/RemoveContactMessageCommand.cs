namespace CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands
{
    public class RemoveContactMessageCommand
    {
        public int Id { get; set; }
        
        public RemoveContactMessageCommand(int id)
        {
            Id = id;
        }
    }
}
