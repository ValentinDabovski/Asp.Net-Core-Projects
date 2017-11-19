namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Models.BindingModels.Camera;
    using CameraBazaar.Models.DataModels.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Route("cameras")]
    public class CameraController : Controller
    {
        private readonly ICameraControllerService service;
        private readonly UserManager<User> userManager;

        public CameraController(ICameraControllerService service,UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [HttpGet]
        [Route(nameof(All))]
        public IActionResult All()
        {
            var allCameras = this.service.GetAllCameras();
            return View(allCameras);
        }

        [Authorize]
        [HttpGet]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CreateCameraBindingModel cameraBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraBindingModel);
            }

            cameraBindingModel.UserId = this.userManager.GetUserId(User);
            this.service.Create(cameraBindingModel);
      

            return RedirectToAction(nameof(All));
        }
    }
}