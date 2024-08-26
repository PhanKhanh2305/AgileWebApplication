using Microsoft.AspNetCore.Mvc;

namespace AgileWebApplication.Components
{
    public class LookSection : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Index");
        }
    }
}
