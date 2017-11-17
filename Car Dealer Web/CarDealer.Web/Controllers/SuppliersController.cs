namespace CarDealer.Web.Controllers
{
    using Services.Enums;
    using Services.Interfaces;
    using ViewModels.Suppliers;

    using Microsoft.AspNetCore.Mvc;

    [Route("suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierControllerService supplierService;

        public SuppliersController(ISupplierControllerService supplierService)
        {
            this.supplierService = supplierService;
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search()
        {
            return this.View();
        }

        [HttpGet("{type}")]
        [Route("all/{type?}")]
        public IActionResult All(string type)
        {
          var supplierType =  type == "importer" ? SuppliersImporterType.Importer : SuppliersImporterType.Local;


          var suppliers =  this.supplierService.FilteredSuppliers(supplierType);

            return View(new AllFilteredSuppliers
            {
                Suppliers = suppliers,
                ImporterType = supplierType
            });
        }
    }
    
}