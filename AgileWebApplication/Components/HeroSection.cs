using Microsoft.AspNetCore.Mvc;

namespace AgileWebApplication.Components
{
    public class HeroSection : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Index");
        }
    }
}
