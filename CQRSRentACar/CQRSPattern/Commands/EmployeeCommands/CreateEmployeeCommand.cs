namespace CQRSRentACar.CQRSPattern.Commands.EmployeeCommands
{
    public class CreateEmployeeCommand
    {
        public string EmployeeNameSurname { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeImageUrl { get; set; }
    }
}
