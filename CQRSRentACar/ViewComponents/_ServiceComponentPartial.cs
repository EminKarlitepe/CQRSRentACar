using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.ViewComponents
{
    public class _ServiceComponentPartial : ViewComponent
    {
        private readonly CQRSContext _context;

        public _ServiceComponentPartial(CQRSContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }
    }
}
