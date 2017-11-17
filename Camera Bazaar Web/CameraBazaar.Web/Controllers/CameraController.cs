namespace CameraBazaar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    public class CameraController : Controller
    {
       
        public IActionResult Index()
        {
           
            return View();
        }
    }
}