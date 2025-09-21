using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommands;
using CQRSRentACar.Entities;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class CreateEmployeeCommandHandler
    {
        private readonly CQRSContext _context;

        public CreateEmployeeCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEmployeeCommand command)
        {
            _context.Employees.Add(new Employee
            {
                EmployeeNameSurname = command.EmployeeNameSurname,
                EmployeePosition = command.EmployeePosition,
                EmployeeImageUrl = command.EmployeeImageUrl
            });

            await _context.SaveChangesAsync();
        }
    }
}
