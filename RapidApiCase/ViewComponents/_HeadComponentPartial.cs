using Microsoft.AspNetCore.Mvc;

namespace RapidApiCase.ViewComponents
{
    public class _HeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
