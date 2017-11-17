namespace CarDealer.Services.Interfaces
{

    using Models.Cars;
    using System.Collections.Generic;

    public interface ICarControllerService
    {
        IEnumerable<CarModel> OrderedCars(string carMake);

        DetailedCarModel DetailedCar(int id);
    }
}
