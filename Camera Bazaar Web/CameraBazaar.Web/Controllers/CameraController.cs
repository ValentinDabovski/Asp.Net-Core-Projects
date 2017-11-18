namespace CameraBazaar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("cameras")]
    public class CameraController : Controller
    {
       
        [HttpGet]
        [Route(nameof(All))]
        public IActionResult All()
        {
           
            return View();
        }


        [HttpGet]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }


    }
}