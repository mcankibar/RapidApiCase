using Microsoft.AspNetCore.Mvc;

namespace RapidApiCase.ViewComponents
{
    public class _ScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
