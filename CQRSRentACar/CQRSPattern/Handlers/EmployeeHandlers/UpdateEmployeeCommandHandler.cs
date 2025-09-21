using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class UpdateEmployeeCommandHandler
    {
        private readonly CQRSContext _context;

        public UpdateEmployeeCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateEmployeeCommand command)
        {
            var value = await _context.Employees.FindAsync(command.EmployeeId);

            value.EmployeeNameSurname = command.EmployeeNameSurname;
            value.EmployeePosition = command.EmployeePosition;
            value.EmployeeImageUrl = command.EmployeeImageUrl;

            await _context.SaveChangesAsync();
        }
    }
}
