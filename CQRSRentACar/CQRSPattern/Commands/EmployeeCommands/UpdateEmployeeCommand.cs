namespace CQRSRentACar.CQRSPattern.Commands.EmployeeCommands
{
    public class UpdateEmployeeCommand
    {
        public int EmployeeId { get; set; }
        public string EmployeeNameSurname { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeImageUrl { get; set; }
    }
}
