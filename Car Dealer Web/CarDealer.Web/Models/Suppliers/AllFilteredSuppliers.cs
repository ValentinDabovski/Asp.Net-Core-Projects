namespace CarDealer.Web.Models.Suppliers
{
    using System.Collections.Generic;
    using Services.Models;
    using Services.Enums;

    public class AllFilteredSuppliers
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        public SuppliersImporterType ImporterType { get; set; }
    }
}
