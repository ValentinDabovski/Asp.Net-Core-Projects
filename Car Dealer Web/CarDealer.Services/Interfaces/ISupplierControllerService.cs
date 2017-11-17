namespace CarDealer.Services.Interfaces
{
    using Enums;
    using Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierControllerService
    {
        IEnumerable<SupplierModel> FilteredSuppliers(SuppliersImporterType importerType);
        IEnumerable<SupplierModel> All();
    }
}
