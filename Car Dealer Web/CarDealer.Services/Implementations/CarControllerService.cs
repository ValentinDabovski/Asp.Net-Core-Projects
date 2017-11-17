namespace CarDealer.Services.Implementations
{
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Cars;
    using Models.Parts;
    using System;
    using System.Collections.Generic;
    using System.Linq;



    public class CarControllerService : ICarControllerService
    {
        private readonly CarDealerDbContext dbContext;

        public CarControllerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<CarModel> OrderedCars(string carMake)

        {
            var carsQuery = this.dbContext.Cars.AsQueryable();

            if (carsQuery.Any(c => c.Make == carMake))
            {
                return carsQuery
                    .Where(c => c.Make == carMake)
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(c => new CarModel()
                    {
                        Id = c.Id,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance,
                        Make = c.Make
                    })
                    .ToList();
            }
            return null;
        }

        /// <summary>
        /// Creates detailed car model
        /// with relational data from db
        /// result is car model with 
        /// information about
        /// car: model
        /// car: make
        /// car: distance travelled
        /// car: parts (relational data)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetailedCarModel DetailedCar(int id)
        {

            var carById = this.dbContext.Cars.Find(id);

            if (carById == null)
            {
                throw new InvalidOperationException("No such Id ");
            }

             // car is not null 
            // get car relational data "parts" from db
            var carwithParts = this.dbContext.Cars
                .Include(carPart => carPart.PartCars)
                .ThenInclude(part => part.Part)
                .FirstOrDefault(car => car.Id == id);


            // bind "parts" data to custom "part" model wich contains only two properties: price and name of the car part
            var carParts = carwithParts.PartCars.Select(c => new PartModel()
            {
                PartName = c.Part.Name,
                PartPrice = c.Part.Price

            }).ToList();

            // create detailed model: car with parts
            var detailedCarModel = new DetailedCarModel()
            {
                Model = carById.Model,
                TravelledDistance = carById.TravelledDistance,
                Make = carById.Make,
                Parts = carParts
            };

            return detailedCarModel;
        }

    }
}
