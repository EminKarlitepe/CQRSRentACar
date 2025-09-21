using CQRSRentACar.Context;
using CQRSRentACar.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.ViewComponents
{
    public class _FeaturesComponentPartial : ViewComponent
    {
        private readonly CQRSContext _context;

        public _FeaturesComponentPartial(CQRSContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _context.Features.ToListAsync();
            return View(features);
        }
    }
}
