using Microsoft.AspNetCore.Mvc;

namespace AgileWebApplication.Components
{
    public class FeatureSection : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Index");
        }
    }
}
