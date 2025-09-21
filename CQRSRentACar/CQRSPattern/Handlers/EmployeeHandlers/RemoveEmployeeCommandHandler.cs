using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommands;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class RemoveEmployeeCommandHandler
    {
        private readonly CQRSContext _context;

        public RemoveEmployeeCommandHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveEmployeeCommand command)
        {
            var value = await _context.Employees.FindAsync(command.Id);
            _context.Employees.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
