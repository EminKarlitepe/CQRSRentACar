using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.ViewComponents
{
    public class _EmployeeComponentPartial : ViewComponent
    {
        private readonly CQRSContext _context;

        public _EmployeeComponentPartial(CQRSContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
    }
}
