using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _ContactComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
