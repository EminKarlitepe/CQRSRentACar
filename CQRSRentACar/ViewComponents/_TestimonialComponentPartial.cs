using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _TestimonialComponentPartial : ViewComponent
    {
        private readonly GetTestimonialQueryHandler _getTestimonialQueryHandler;

        public _TestimonialComponentPartial(GetTestimonialQueryHandler getTestimonialQueryHandler)
        {
            _getTestimonialQueryHandler = getTestimonialQueryHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = await _getTestimonialQueryHandler.Handle();
            return View(testimonials);
        }
    }
}
