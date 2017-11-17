namespace CarDealer.Web.Controllers
{
    using Services.Models.Customers;
    using Services.Enums;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Customers;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerControllerService customerService;

        public CustomersController(ICustomerControllerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet("{order}")]
        [Route("all/{order?}")]
        public IActionResult All(string order)
        {

            var orderDirection = order == "descending" ? OrderDirection.Descending : OrderDirection.Ascending;

            var customers = this.customerService.OrderedCustomers(orderDirection);

            return this.View(new AllCustomersModel()
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            var customerDetailedModel = this.customerService.DetailedCustomerModel(id);

            return this.View(customerDetailedModel);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(customerModel);
            }

            this.customerService.AddCustomer(customerModel);
            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var customerEdit = this.customerService.Edit(id);

            return View(customerEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, CustomerEditModel editModel)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            this.customerService.Edit(id, editModel);

            return RedirectToAction(nameof(All));

        }

  
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            
            return View(this.customerService.GetCustomerByIdToDelete(id));
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return View("Error");
            }
           
            this.customerService.ConfirmCustomerDelete(id);
            return RedirectToAction(nameof(All));
        }

    }
}