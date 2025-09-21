using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
