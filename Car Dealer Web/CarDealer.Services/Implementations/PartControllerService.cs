using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Services.Implementations
{
    using Data;
    using Interfaces;
    using Models.Parts;
    using Data.Models;

    public class PartControllerService : IPartControllerService
    {
        private readonly CarDealerDbContext dbContext;

        public PartControllerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }




        public void CreatePart(string name, double price, int quantity, int suppplierId)
        {
            var part = new Part()
            {
                Name = name,
                Price = price,
                Quantity = quantity > 0 ? quantity : 1,
                SupplierId = suppplierId
            };

            this.dbContext.Add(part);
            this.dbContext.SaveChanges();
        }

        public EditPartModel Edit(int? id)
        {
            if (id <= 0)
            {
                return null;
            }


            var editPart = this.dbContext.Parts
                .Where(p => p.Id == id)
                .Select(p => new EditPartModel()
                {
                    PartName = p.Name,
                    PartPrice = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .FirstOrDefault();


            return editPart;
        }

        public DeletePartModel Delete(int? id)
        {
            if (id != null)
            {
                return this.dbContext.Parts
                    .Where(p => p.Id == id)
                    .Select(p => new DeletePartModel()
                    {
                        Id = p.Id,
                        PartName = p.Name,
                        PartPrice = p.Price

                    })
                    .FirstOrDefault();
            }

            return null;
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                var partToDelete = this.dbContext.Parts.Find(id);

                this.dbContext.Parts.Remove(partToDelete);

                this.dbContext.SaveChanges();
            }
        }

        public void Edit(int id, EditPartModel model)
        {
            if (id > 0 || model != null)
            {
                var partToEdit = this.dbContext.Parts.Find(id);

                partToEdit.Price = model.PartPrice;

                partToEdit.Quantity = model.Quantity;

                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<AllPartsModel> All()
        {
            return this.dbContext.Parts
            .OrderByDescending(p => p.Id)
            .Select(p => new AllPartsModel()
            {
                Id = p.Id,
                PartName = p.Name,
                PartPrice = p.Price,
                Quantity = p.Quantity,
                SupplierName = p.Supplier.Name
            })
            .ToList();
        }
    }
}
