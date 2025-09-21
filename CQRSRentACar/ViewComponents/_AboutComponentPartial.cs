using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.ViewComponents
{
    public class _AboutComponentPartial : ViewComponent
    {
        private readonly CQRSContext _context;

        public _AboutComponentPartial(CQRSContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
    }
}
