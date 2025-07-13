using Microsoft.AspNetCore.Mvc;

namespace RapidApiCase.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
