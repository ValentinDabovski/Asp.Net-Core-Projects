namespace CarDealer.Web.Controllers
{
    using ViewModels.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Route("cars")]
    public class CarsController : Controller
    {
        private ICarControllerService carControllerService;

        public CarsController(ICarControllerService carControllerService)
        {
            this.carControllerService = carControllerService;
        }


        [HttpGet]
        [Route("{id}/details")]
        public IActionResult Details(int id)
        {
         var detailedCar = this.carControllerService.DetailedCar(id);

            return this.View(detailedCar);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search()
        {
            return this.View();
        }

        [HttpGet("{carMake}")]
        [Route("all/{carMake?}")]
        public IActionResult All([FromForm] string carMake)
        {
          var cars = this.carControllerService.OrderedCars(carMake);

            if (cars == null)
            {
                return this.NotFound("No such car make in database.");
               
            }

            return this.View(new AllCarsModel()
            {
                Cars = cars,
                CarMake = carMake
            });
        }
    }
}