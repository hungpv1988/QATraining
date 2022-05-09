using Microsoft.AspNetCore.Mvc;

namespace StudentAppMvc.Controllers
{
    public class MingStudent : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
