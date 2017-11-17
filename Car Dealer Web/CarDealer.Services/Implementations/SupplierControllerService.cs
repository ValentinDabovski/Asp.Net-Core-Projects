namespace CarDealer.Services.Implementations
{
    using Data;
    using Enums;
    using Interfaces;
    using Models.Suppliers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierControllerService : ISupplierControllerService
    {
        private readonly CarDealerDbContext dbContext;

        public SupplierControllerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SupplierModel> FilteredSuppliers(SuppliersImporterType importerType)
        {
            var suppliersQuery = this.dbContext.Suppliers.AsQueryable();

            switch (importerType)
            {
                case SuppliersImporterType.Importer:
                    suppliersQuery = suppliersQuery
                        .Where(s => s.IsImporterType)
                        .OrderBy(s => s.Name);
                    break;

                case SuppliersImporterType.Local:
                    suppliersQuery = suppliersQuery.Where(s => !s.IsImporterType)
                        .OrderBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException("No such supplier type.");
            }

            return suppliersQuery.Select(s => new SupplierModel()
            {
                Name = s.Name,
                Id = s.Id,
                PartsCount = s.Parts.Count
            })
            .ToList();
        }

        public IEnumerable<SupplierModel> All()
        {
            return this.dbContext.Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel()
                {
                    Name = s.Name,
                    PartsCount = s.Parts.Count,
                    Id = s.Id
                });
        }
    }
}
