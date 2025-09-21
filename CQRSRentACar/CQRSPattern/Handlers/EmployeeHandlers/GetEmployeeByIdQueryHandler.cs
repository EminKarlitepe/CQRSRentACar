using System.Threading.Tasks;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.EmployeeQueries;
using CQRSRentACar.CQRSPattern.Results.EmployeeResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler
    {
        private readonly CQRSContext _context;

        public GetEmployeeByIdQueryHandler(CQRSContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeeQueryResult> Handle(GetEmployeeByIdQuery query)
        {
            var value = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == query.Id);

            return new GetEmployeeQueryResult
            {
                EmployeeId = value.EmployeeId,
                EmployeeNameSurname = value.EmployeeNameSurname,
                EmployeePosition = value.EmployeePosition,
                EmployeeImageUrl = value.EmployeeImageUrl
            };
        }
    }
}
