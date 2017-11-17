namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using Enums;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Cars;
    using Models.Customers;
    using Models.Parts;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class CustomerControllerService : ICustomerControllerService
    {
        private readonly CarDealerDbContext dbContext;

        public CustomerControllerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection orderDirection)
        {
            var customerQuery = dbContext.Customers.AsQueryable();

            switch (orderDirection)
            {
                case OrderDirection.Ascending:
                    customerQuery = customerQuery.OrderByDescending(c => c.Id).ThenBy(c => c.BirthDate).ThenBy(c => c.Name);
                    break;

                case OrderDirection.Descending:
                    customerQuery = customerQuery.OrderByDescending(c => c.Id).ThenByDescending(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                default:

                    throw new InvalidOperationException($"Invalid order direction {orderDirection}");

            }
            return customerQuery.Select(c => new CustomerModel
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver

            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerDetailedModel DetailedCustomerModel(int id)
        {
            var customerQuery = this.dbContext.Customers.AsQueryable();


            bool existId = customerQuery.Any(c => c.Id == id);

            if (!existId)
            {
                throw new InvalidOperationException($"Invalid id {id}.");
            }

            var customerDetails = customerQuery
                .Include(s => s.Sales)
                .ThenInclude(c => c.Car)
                .ThenInclude(cp => cp.PartCars)
                .ThenInclude(p => p.Part)
                .FirstOrDefault(cus => cus.Id == id);


            var customerCars = customerDetails.Sales.Select(c => new CarModel()
            {
                Model = c.Car.Model,
                Make = c.Car.Make,
                TravelledDistance = c.Car.TravelledDistance
            }).ToList();


            var partCars = customerDetails.Sales.Select(c => c.Car.PartCars).ToList();

            var parts = new List<PartModel>();

            foreach (var partCar in partCars)
            {
                foreach (var part in partCar)
                {
                    var partModel = new PartModel()
                    {
                        PartName = part.Part.Name,
                        PartPrice = part.Part.Price
                    };

                    parts.Add(partModel);

                }
            }



            var detailedCustomer = new CustomerDetailedModel()
            {
                BirthDate = customerDetails.BirthDate,
                IsYoungDriver = customerDetails.IsYoungDriver,
                Name = customerDetails.Name,
                TotalCarsBought = customerDetails.Sales.Count,
                Cars = customerCars,
                Parts = parts
            };

            return detailedCustomer;
        }

        public void AddCustomer(CustomerModel customer)
        {
            if (customer != null)
            {
                bool isCustomerYoung = DateTime.Now.Year - customer.BirthDate.Year <= 23;

                this.dbContext.Customers.Add(new Customer()
                {
                    BirthDate = customer.BirthDate,
                    IsYoungDriver = isCustomerYoung,
                    Name = customer.Name
                });

                this.dbContext.SaveChanges();
            }
        }

        public CustomerModel Edit(int? id)
        {
            bool idExist = this.dbContext.Customers.Any(c => c.Id == id);

            if (id < 0 || !idExist)
            {
                return null;
            }

            var customerToEdit = this.dbContext.Customers.Find(id);

            return new CustomerModel()
            {   

                BirthDate = customerToEdit.BirthDate,
                Name = customerToEdit.Name
            };
        }

        public void Edit(int id, CustomerEditModel model)
        {
            var customerToEdit = this.dbContext.Customers.Find(id);

            customerToEdit.Name = model.Name;

            customerToEdit.BirthDate = model.BirtDate;
            
            this.dbContext.SaveChanges();
        }

        public CustomerDeleteModel GetCustomerByIdToDelete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException("Invalid operations no such id");
            }

            var customerToDelete = this.dbContext.Customers.Find(id);

            return new CustomerDeleteModel()
            {
                Id = customerToDelete.Id,
                Name = customerToDelete.Name
            };

        }

        public void ConfirmCustomerDelete(int id)
        {
            var customerToDelete = this.dbContext.Customers.Find(id);
            this.dbContext.Customers.Remove(customerToDelete);
            this.dbContext.SaveChanges();
        }
    }
}
