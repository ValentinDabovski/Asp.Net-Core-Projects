namespace CarDealer.Web.ViewModels.Suppliers
{
    using Services.Enums;
    using Services.Models.Suppliers;
    using System.Collections.Generic;

    public class AllFilteredSuppliers
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        public SuppliersImporterType ImporterType { get; set; }
    }
}
