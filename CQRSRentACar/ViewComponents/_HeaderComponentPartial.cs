using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _HeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
