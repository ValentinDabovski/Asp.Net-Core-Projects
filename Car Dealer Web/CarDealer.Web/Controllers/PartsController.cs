namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Services.Models.Parts;

    [Route("parts")]
    public class PartsController : Controller
    {
        private readonly IPartControllerService partsService;
        private readonly ISupplierControllerService suppliersService;

        public PartsController(IPartControllerService partsService, ISupplierControllerService suppliersService)
        {
            this.partsService = partsService;
            this.suppliersService = suppliersService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult All()
        {
            var parts = this.partsService.All();
            return View(parts);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View(new PartAddModel()
            {
                Suppliers = this.GetSuppliersListItems()
            });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(PartAddModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetSuppliersListItems();
                return View(model);
            }

            this.partsService.CreatePart(
                model.PartName,
                model.PartPrice,
                model.Quantity,
                model.SupplierId);

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            var model = this.partsService.Edit(id);
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, EditPartModel model)
        {
            if (id <= 0 || model == null)
            {
                return BadRequest();
            }

            this.partsService.Edit(id, model);

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                return View(this.partsService.Delete(id));
            }

            return NotFound();
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            if (id > 0)
            {
                this.partsService.Delete(id);

                return RedirectToAction(nameof(All));
            }

            return NotFound();
        }

        private IEnumerable<SelectListItem> GetSuppliersListItems()
        {
            return this.suppliersService
            .All()
            .Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
        }
    }
}