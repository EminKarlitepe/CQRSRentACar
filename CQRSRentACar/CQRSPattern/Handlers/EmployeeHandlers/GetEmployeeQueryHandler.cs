using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.EmployeeResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeQueryHandler
    {
        private readonly CQRSContext _context;

        public GetEmployeeQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<List<GetEmployeeQueryResult>> Handle()
        {
            var values = await _context.Employees.AsNoTracking().ToListAsync();

            return values.Select(x => new GetEmployeeQueryResult
            {
                EmployeeId = x.EmployeeId,
                EmployeeNameSurname = x.EmployeeNameSurname,
                EmployeePosition = x.EmployeePosition,
                EmployeeImageUrl = x.EmployeeImageUrl
            }).ToList();
        }
    }
}
