namespace CameraBazaar.Web.Controllers
{
    using Models.BindingModels.Camera;
    using Models.DataModels.Identity;
    using Models.ViewModels.Camera;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using System.Threading.Tasks;

    [Authorize]
    [Route("cameras")]
    public class CameraController : Controller
    {
        private readonly ICameraControllerService service;
        private readonly UserManager<User> userManager;

        public CameraController(ICameraControllerService service, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [AllowAnonymous]
        [HttpGet("{search?}")]
        [Route("all/{search?}")]
        public IActionResult All(string search)
        {
            var allCameras = this.service.GetAllCameras(search);

            if (allCameras != null)
            {
                return View(allCameras);
            }

            return View("Error");

        }

        [HttpGet]
        [Route("add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create(CreateCameraBindingModel cameraBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraBindingModel);
            }
            cameraBindingModel.UserId = this.userManager.GetUserId(this.User);
            this.service.Create(cameraBindingModel);

            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            DetailCameraView detailedCameraView = await this.service.Details(id);

            if (detailedCameraView != null)
            {
                return View(detailedCameraView);
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("userdetails/{id}")]
        public async Task<IActionResult> UserDetails(string userId)
        {
            var guestUserId = this.userManager?.GetUserId(this.User);

            UserDetailsViewModel userDetailsViewModel = await this.service.GetUserDetail(userId, guestUserId);

            if (userDetailsViewModel != null)
            {
                return View(userDetailsViewModel);
            }
            return NotFound();
        }


        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var userId = this.GetuserId();

            var userCanEditCamera = this.service.UserCanDrestoryCamera(id, userId);

            if (!userCanEditCamera)
            {
                TempData.Add("ErrorMessage", "You have no permissions to edit this camera");
                return View("Error");
            }


            var viewModel = this.service.Edit(id);

            if (viewModel != null)
            {
                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, EditCameraBindingModels cameraModel)
        {
            var userId = this.GetuserId();

            var userCanEditCamera = this.service.UserCanDrestoryCamera(id, userId);

            if (!userCanEditCamera)
            {
                TempData.Add("ErrorMessage", "You have no permissions to edit this camera");
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.service.Edit(id, cameraModel);

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Route("mycameras")]
        public async Task<IActionResult> UserAllCameras()
        {
            var userId = this.GetuserId();

            if (userId != null)
            {
                var viewModel = await this.service.GetUserDetail(userId, userId);

                return View(nameof(UserDetails), viewModel);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            var userId = this.GetuserId();

            var userCanDeleteCamera = this.service.UserCanDrestoryCamera(id, userId);

            if (!userCanDeleteCamera)
            {
                TempData.Add("ErrorMessage", "You have no permissions to delete this camera.");
                return View("Error");
            }

            CameraDeleteViewModel cameraModelToDelete = this.service.Delete(id);
            if (cameraModelToDelete != null)
            {
                return View(cameraModelToDelete);
            }

            return View("Error");
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Destroy(int id)
        {
            var userId = this.GetuserId();

            if (id > 0)
            {
                var userCanDestoryCamera = this.service.UserCanDrestoryCamera(id, userId);

                if (userCanDestoryCamera)
                {
                    this.service.Destroy(id);

                    return RedirectToAction(nameof(UserAllCameras));
                }

            }

            return BadRequest();
        }

        private string GetuserId()
        {
            var userId = userManager.GetUserId(this.User);

            return userId;
        }
    }
}