using Microsoft.AspNetCore.Mvc;

namespace RapidApiCase.ViewComponents
{
    public class _HeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
